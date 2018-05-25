﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanatana.Notifications.Composing.Templates;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sanatana.Notifications.DAL;
using AutoMapper;
using Sanatana.Notifications.DAL.EntityFrameworkCore.AutoMapper;
using Sanatana.Notifications.DAL.EntityFrameworkCore.Context;
using Sanatana.EntityFrameworkCore;
using Sanatana.EntityFrameworkCore.Commands;
using Sanatana.EntityFrameworkCore.Commands.Merge;
using Sanatana.Notifications.DAL.Interfaces;
using Sanatana.Notifications.DAL.Entities;
using Sanatana.Notifications.DAL.Results;

namespace Sanatana.Notifications.DAL.EntityFrameworkCore
{
    public class SqlDispatchTemplateQueries : IDispatchTemplateQueries<long>
    {
        //fields        
        protected SqlConnectionSettings _connectionSettings;
        protected ISenderDbContextFactory _dbContextFactory;
        protected IMapper _mapper;


        //init
        public SqlDispatchTemplateQueries(SqlConnectionSettings connectionSettings
            , ISenderDbContextFactory dbContextFactory, INotificationsMapperFactory mapperFactory)
        {
            _connectionSettings = connectionSettings;
            _dbContextFactory = dbContextFactory;
            _mapper = mapperFactory.GetMapper();
        }
        

        //insert
        public virtual async Task Insert(List<DispatchTemplate<long>> items)
        {
            List<DispatchTemplateLong> mappedList = items
                .Select(_mapper.Map<DispatchTemplateLong>)
                .ToList();

            using (Repository repository = new Repository(_dbContextFactory.GetDbContext()))
            {
                InsertCommand<DispatchTemplateLong> insertCommand = repository.Insert<DispatchTemplateLong>();
                insertCommand.Insert
                    .ExcludeProperty(x => x.DispatchTemplateId);
                insertCommand.Output
                    .IncludeProperty(x => x.DispatchTemplateId);
                int changes = await insertCommand.ExecuteAsync(mappedList).ConfigureAwait(false);

                for (int i = 0; i < mappedList.Count; i++)
                {
                    items[i].DispatchTemplateId = mappedList[i].DispatchTemplateId;
                }
            }
        }


        //select
        public virtual async Task<TotalResult<List<DispatchTemplate<long>>>> Select(int page, int pageSize)
        {
            RepositoryResult<DispatchTemplateLong> response = null;
            
            using (Repository repository = new Repository(_dbContextFactory.GetDbContext()))
            {
                response = await repository
                    .SelectPageAsync<DispatchTemplateLong, long>(page, pageSize, true,
                        x => true, x => x.ComposerSettingsId, true)
                    .ConfigureAwait(false);                
            }

            List<DispatchTemplate<long>> mappedItems = response.Data
                .Select(_mapper.Map<DispatchTemplate<long>>)
                .ToList();
            return new TotalResult<List<DispatchTemplate<long>>>(mappedItems, response.TotalRows);
        }

        public virtual async Task<DispatchTemplate<long>> Select(long dispatchTemplatesId)
        {
            DispatchTemplateLong item = null;
           
            using (SenderDbContext context = _dbContextFactory.GetDbContext())
            {
                item = await context.DispatchTemplates
                    .FirstOrDefaultAsync(x => x.DispatchTemplateId == dispatchTemplatesId)
                    .ConfigureAwait(false);
            }

            DispatchTemplate<long> mappedItem = item == null
                ? null
                : _mapper.Map<DispatchTemplate<long>>(item);
            return mappedItem;
        }

        public virtual async Task<List<DispatchTemplate<long>>> SelectForComposerSettings(long composerSettingsId)
        {
            List<DispatchTemplateLong> items = null;
            
            using (SenderDbContext context = _dbContextFactory.GetDbContext())
            {
                items = await context.DispatchTemplates
                    .Where(x => x.ComposerSettingsId == composerSettingsId)
                    .ToListAsync()
                    .ConfigureAwait(false);
            }

            List<DispatchTemplate<long>> mappedItems = items
                .Select(_mapper.Map<DispatchTemplate<long>>).ToList();
            return mappedItems;
        }


        //update
        public virtual async Task Update(List<DispatchTemplate<long>> items)
        {
            List<DispatchTemplateLong> mappedItems = items
                .Select(_mapper.Map<DispatchTemplateLong>)
                .ToList();
            
            using (Repository repository = new Repository(_dbContextFactory.GetDbContext()))
            {
                string tvpName = TableValuedParameters.GetFullTVPName(_connectionSettings.Schema, TableValuedParameters.DISPATCH_TEMPLATE_TYPE);
                MergeCommand<DispatchTemplateLong> merge = repository.MergeTVP(mappedItems, tvpName);
                merge.Compare.IncludeProperty(p => p.DispatchTemplateId);
                int changes = await merge.ExecuteAsync(MergeType.Update).ConfigureAwait(false);                
            }
        }


        //delete
        public virtual async Task Delete(List<DispatchTemplate<long>> items)
        {
            List<long> ids = items.Select(p => p.ComposerSettingsId)
                .Distinct()
                .ToList();

            using (Repository repository = new Repository(_dbContextFactory.GetDbContext()))
            {
                int changes = await repository.DeleteManyAsync<DispatchTemplateLong>(
                    x => ids.Contains(x.ComposerSettingsId))
                    .ConfigureAwait(false);                
            }
        }

    }
}
