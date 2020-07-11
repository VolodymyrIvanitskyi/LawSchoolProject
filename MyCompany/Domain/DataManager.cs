using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain
{
    public class DataManager
    {
        public ITextFieldRepository TextFields { get; set; }
        public IServiceItemRepository ServiceItems { get; set; }

        public DataManager(ITextFieldRepository textFieldRepository, IServiceItemRepository serviceItemRepository)
        {
            TextFields = textFieldRepository;
            ServiceItems = serviceItemRepository;
        }
    }
}
