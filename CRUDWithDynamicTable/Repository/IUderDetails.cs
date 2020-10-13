using CRUDWithDynamicTable.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDWithDynamicTable.Repository
{
    public interface IUserDetails
    {
        IEnumerable<UserDetails> GetUserDetails();
        void GetInsertDetail(UserDetails insert);

        void GetDeleteDetail(int? id);

        void EditUserDetails(UserDetails insert);

        EditViewModel GetEditDetails(int Id);


    }
}