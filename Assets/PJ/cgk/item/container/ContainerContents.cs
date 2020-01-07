using fNbt;
using System;

/// <summary>
/// Stores the contents of a contain and provides methods to interact with the contents.
/// </summary>
public class ContainerContents<T> where T : IItemBase {

    public readonly int width;
    public readonly int height;
    private T[] items;

    public ContainerContents(int width, int height) {
        if(width <= 0) {
            throw new Exception("width must be greater than 0!");
        }
        if(height <= 0) {
            throw new Exception("height must be greater than 0!");
        }

        this.width = width;
        this.height = height;
        this.items = new T[this.width * this.height];
    }

    /// <summary>
    /// Returns the item at the passed index.
    /// </summary>
    public T getItem(int index) {
        return this.items[index];
    }

    /// <summary>
    /// Returns the item at (x, y).
    /// </summary>
    public T getItem(int x, int y) {
        return this.items[x + this.width * y];
    }

    /// <summary>
    /// Sets the item at (x, y).
    /// </summary>
    public void setItem(int x, int y, T item) {
        this.items[x + this.width * y] = item;
    }

    /// <summary>
    /// Sets the item at (x, y).
    /// </summary>
    public void setItem(int index, T item) {
        this.items[index] = item;
    }

    public T[] getRawItemArray() {
        return this.items;
    }

    /// <summary>
    /// Adds the passed Item to the container and returns null.  If there isn't space, the item passed in is returned.
    /// </summary>
    public T addItem(T item) {
        for(int i = 0; i < this.items.Length; i++) {
            if(this.items[i] == null) {
                this.items[i] = item;
                return default(T);
            }
        }
        return item;
    }

    /// <summary>
    /// Returns true if the container is full.
    /// </summary>
    public bool isFull() {
        return this.getFreeSpots() <= 0;
    }

    /// <summary>
    /// Returns the number of free spots.
    /// </summary>
    public int getFreeSpots() {
        int spots = 0;
        for(int i = 0; i < this.items.Length; i++) {
            if(this.items[i] == null) {
                spots++;
            }
        }
        return spots;
    }

    /// <summary>
    /// Checks if the container contains the passed item.
    /// If it does, true is returned and the parameter index is set to the item's index.
    /// If not, false is returned and index is set to -1;
    /// </summary>
    public bool containsItem(ItemData item, out int index) {
        if(item == null) {
            index = -1;
            return false;
        }

        for(int i = 0; i < this.items.Length; i++) {
            if(this.items[i].getData() == item) {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
    }

    /*
    public virtual NbtCompound writeToNbt(NbtCompound tag) {
        NbtList list = new NbtList("items", NbtTagType.Compound);

        for(int i = 0; i < this.items.Length; i++) {
            if(this.items[i] != null) {
                NbtCompound tagStack = this.items[i].writeToNbt();
                tagStack.Add(new NbtInt("slotIndex", i));
                list.Add(tagStack);
            }
        }
        tag.Add(list);
        return tag;
    }

    public virtual void readFromNbt(NbtCompound tag) {
        foreach(NbtCompound compound in tag.Get<NbtList>("items")) {
            this.items[compound.Get<NbtInt>("slotIndex").IntValue] = new ItemStack(compound);
        }
    }
    */
}