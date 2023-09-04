//This code demonstrates a basic implementation of a blockchain with the ability to add blocks and print the blockchain's contents.
//The purpose of this code is to show an example of commenting and XML Documentation in C# for learning purposes only.
//This code was written by Victor Chiemelie Ndulue and licensed under MIT license.


using System.Security.Cryptography;
using System.Text;



///<summary>
///Initializes a new instance of <see cref="Blockchain"/> 
///</summary>
Blockchain blockChain = new Blockchain();
///<summary>
///Calls the <see cref="Blockchain.AddBlockToChain(DateTime, string, string)"/> and adds blocks to list of blockChain
///Calls the <see cref="Blockchain.GetLatestBlock().Hash"/> to get the hash value of the last Block
/// </summary>
blockChain.AddBlockToChain(DateTime.Now, "Transaction 1", blockChain.GetLatestBlock().Hash);
blockChain.AddBlockToChain(DateTime.Now, "Transaction 2", blockChain.GetLatestBlock().Hash);

//Calling method to print the created blocks
blockChain.PrintChain();


/// <summary>
/// The <c>Block</c> class 
/// Represents an individual block in the blockchain.
/// <list type="bullet">
/// <item>
/// <term>CalculateHash</term>
/// <description>Encodes and hashes the summation of TimeStamp, Data and PreviousHash. 
/// Returns a string value assigned to the Hash property</description>
/// </item>
/// </list>
/// </summary>
public class Block
{
    //Stores the timestamp when the block was created.
    public DateTime Timestamp { get; private set; }
    //Contains data associated with the block (e.g., transaction data).
    public string Data { get; private set; }
    // Stores the hash of the previous block in the chain.
    public string PreviousHash { get; private set; }
    //Represents the hash of the current block.
    public string Hash { get; private set; }


    /// <summary>
    /// Initializes a new instance of <see cref="Block"/>
    /// </summary>
    /// <param name="timestamp"> The time of creation</param>
    /// <param name="data">data associated with block</param>
    /// <param name="previousHash">value of hash before hash to be created</param>
    public Block(DateTime timestamp, string data, string previousHash)
    {
        Timestamp = timestamp;
        Data = data;
        PreviousHash = previousHash;
        Hash = CalculateHash();
    }
    //adds Timestamp, Data and PreviousHash, encodes and hashes them returnin the hashed value as string.
    /// <summary>
    /// This method computes the hash of the block based on its Timestamp, Data, and PreviousHash.
    /// It uses the SHA-256 cryptographic hash function to generate the hash.
    /// </summary>
    /// <returns>String value of hashed byte array</returns>
    public string CalculateHash()
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            string input = Timestamp + Data + PreviousHash;
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}

/// <summary>
/// The <c>Blockchain</c> class
///  Contains all method invloved in creating a blockchain(a list of blocks).
/// <list type="bullet">
/// <item>
/// <term>Blockchain</term>
/// <description>Constructor</description>
/// </item>
/// <item>
/// <term>InitializeBlockchain</term>
/// <description>Initializes an instance and adds it to the blockchain</description>
/// </item>
/// <item> 
/// <term>AddBlockToChain <see cref="AddBlockToChain(DateTime, string, string)"/></term>
/// <description>Uses the block constructor to create a block and adds it to a chain list</description>
/// </item>
/// <item>
/// <term>GetLatestBlock <see cref="GetLatestBlock"/></term>
/// <description>returns the value of last hash</description>
/// </item>
/// <item>
/// <term>PrintChain <see cref="PrintChain"/></term>
/// <description>Iterates through the chain list and prints each block property value to the console</description>
/// </item>
/// </list>
/// </summary>
public class Blockchain
{
    /// <summary>
    /// This private field holds the list of blocks, forming the blockchain.
    /// </summary>

    private List<Block> chain;

    ///<summary>
    /// Initializes a new instance of the <see cref="Blockchain"/> class.
    ///</summary>
    public Blockchain()
    {
        chain = new List<Block>();
        InitializeBlockchain();
    }

    ///<summary>
    /// Initializes the blockchain with a genesis block.
    ///</summary>
    private void InitializeBlockchain()
    {
        DateTime timestamp = DateTime.Now;
        string data = "Genesis Block";
        string previousHash = "0";
        AddBlockToChain(timestamp, data, previousHash);
    }

    ///<summary>
    /// Adds a new block to the blockchain with the provided timestamp, data, and previousHash.
    ///</summary>
    /// <param name="timestamp">The timestamp of the new block.</param>
    /// <param name="data">The data associated with the new block.</param>
    /// <param name="previousHash">The hash of the previous block.</param>
    public void AddBlockToChain(DateTime timestamp, string data, string previousHash)
    {
        Block block = new Block(timestamp, data, previousHash);
        chain.Add(block);
    }

    ///<summary>
    /// Gets the latest block in the blockchain.
    ///</summary>
    /// <returns>The latest block.</returns>
    public Block GetLatestBlock()
    {
        return chain[chain.Count - 1];
    }

    ///<summary>
    /// Prints the details of all blocks in the blockchain.
    ///</summary>
    public void PrintChain()
    {
        foreach (Block block in chain)
        {
            Console.WriteLine("Timestamp: " + block.Timestamp);
            Console.WriteLine("Data: " + block.Data);
            Console.WriteLine("Previous Hash: " + block.PreviousHash);
            Console.WriteLine("Hash: " + block.Hash);
            Console.WriteLine();
        }
    }
}