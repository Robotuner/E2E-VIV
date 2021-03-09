using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ElectionModels.Misc
{
    public class BlockChain
    {
        public IList<Block> Chain { get; set; }
        public int Difficulty { get; set; } = 2;

        public BlockChain()
        {
            InitializeChain();
            AddGenesisBlock();
        }

        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            return new Block(DateTime.Now, null, "{}");
        }

        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public async Task AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;

            await block.Mine(this.Difficulty);
            Chain.Add(block);
        }

        public bool IsValid()
        {
            int lastNdx = Chain.Count - 1;
            Block currentBlock = Chain[lastNdx];
            Block previousBlock = Chain[lastNdx - 1];
            string calcHash = currentBlock.CalculateHash();
            if (currentBlock.Hash != calcHash || currentBlock.PreviousHash != previousBlock.Hash)
            {
                return false;
            }
            return true;
        }
    }
}
