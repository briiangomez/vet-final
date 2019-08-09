using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_Core.Repositories;
using Veterinaria_Services;
using Vet_Data.Models;

namespace Vet_BLL
{
    public class ItemBLL
    {
        public readonly ItemRepository _ItemRepository =  new ItemRepository();

        public ItemBLL()
        {
           // _ItemRepository = ItemRepository;
        }
        public List<Item> ObtenerItems()
        {
            List<Item> Items = new List<Item>();
            try
            {
                Items = _ItemRepository.List().ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Items;
        }

        public void Alta(Item Item)
        {
            try
            {
                _ItemRepository.Add(Item);
                _ItemRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public Item ObtenerItem(int id)
        {
            Item Item = new Item();
            try
            {
                Item = _ItemRepository.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return Item;
        }

        public void Actualizar(Item Item)
        {
            try
            {
                _ItemRepository.Update(Item);
                _ItemRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        public void Borrar(int id)
        {
            try
            {
                _ItemRepository.Delete(id);
                _ItemRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
    }
}
