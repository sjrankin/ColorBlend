using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlend.Compositing
{
    public class DisplayList : IEnumerable
    {
        public DisplayList ()
        {
            ItemList = new List<DisplayItemBase>();
        }

        public void Add (DisplayItemBase NewItem)
        {
            if (NewItem == null)
                throw new ArgumentNullException("NewItem");
            ItemList.Add(NewItem);
        }

        public void AddRange (List<DisplayItemBase> Items)
        {
            ItemList.AddRange(Items);
        }

        public void Insert (int Index, DisplayItemBase Item)
        {
            ItemList.Insert(Index, Item);
        }

        public void Remove (DisplayItemBase ItemToRemove)
        {
            ItemList.Remove(ItemToRemove);
        }

        public void RemoveAt (int Index)
        {
            ItemList.RemoveAt(Index);
        }

        public void RemoveAll (params ItemTypes[] Types)
        {
            List<ItemTypes> TypesToRemove = Types.ToList();
            List<DisplayItemBase> NewList = new List<DisplayItemBase>();
            foreach (DisplayItemBase Item in ItemList)
            {
                if (TypesToRemove.Contains(Item.ItemType))
                    continue;
                NewList.Add(Item);
            }
            ItemList.Clear();
            ItemList = NewList;
        }

        public List<DisplayItemBase> SortItems (HashSet<ItemTypes> Order)
        {
            ClearMarkedItems();
            List<DisplayItemBase> Sorted = new List<DisplayItemBase>();
            foreach(ItemTypes ItemType in Order)
            {
                foreach(DisplayItemBase Item in ItemList)
                {
                    if ((Item.ItemType == ItemType) && (!Item.Marked))
                    {
                        Sorted.Add(Item);
                        Item.Marked = true;
                    }
                }
            }
            return Sorted;
        }

        private void ClearMarkedItems()
        {
            foreach (DisplayItemBase Item in ItemList)
                Item.Marked = false;
        }

        public void Reverse ()
        {
            ItemList.Reverse();
        }

        public DisplayItemBase Last ()
        {
            return ItemList.Last();
        }

        public List<DisplayItemBase> Copy (bool Reversed = false)
        {
            DisplayItemBase[] ItemArray = new DisplayItemBase[ItemList.Count];
            ItemList.CopyTo(ItemArray);
            List<DisplayItemBase> Copied = ItemArray.ToList();
            if (Reversed)
                Copied.Reverse();
            return Copied;
        }

        public void Clear ()
        {
            ItemList.Clear();
        }

        internal List<DisplayItemBase> ItemList { get; set; }

        public IEnumerator GetEnumerator ()
        {
            foreach (DisplayItemBase Item in ItemList)
                yield return Item;
        }

        public IEnumerator ReverseEnumeration ()
        {
            List<DisplayItemBase> Local = Copy(true);
            foreach (DisplayItemBase Item in Local)
                yield return Item;
        }

        public IEnumerator ReturnIf (params ItemTypes[] Types)
        {
            List<ItemTypes> TypesList = Types.ToList();
            foreach (DisplayItemBase Item in ItemList)
                if (TypesList.Contains(Item.ItemType))
                    yield return Item;
        }

        public IEnumerator ReturnIfNot (params ItemTypes[] Types)
        {
            List<ItemTypes> TypesList = Types.ToList();
            foreach (DisplayItemBase Item in ItemList)
                if (!TypesList.Contains(Item.ItemType))
                    yield return Item;
        }
    }
}
