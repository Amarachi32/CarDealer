using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Car.DLL.Entities
{
    public class ShoppingCart : BaseEntity
    {

       // public string UserId { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
    }
}
