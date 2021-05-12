using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Tutorials
{
    public class ComplexAbstract
    {
        public ComplexAbstract()
        {
            WarmeDrank koffie = new Koffie();
            Console.WriteLine(koffie.GetDescription());
            WarmeDrank choco = new Chocomelk();
            choco = new Melk(choco);
            choco = new Room(choco);
            Console.WriteLine(choco.GetDescription());
            Console.ReadKey();
        }
    }

    //=======================================================================================
    public class Chocomelk : WarmeDrank
    {
        public Chocomelk()
        {
            description = "chocomelk";
        }
    }
    public class Koffie : WarmeDrank
    {
        public Koffie()
        {
            description = "koffie";
        }
    }
    public class Thee : WarmeDrank
    {
        public Thee()
        {
            description = "Thee";
        }
    }
    public abstract class WarmeDrankDecorator : WarmeDrank
    {
        public abstract override string GetDescription();
    }
    public class Melk : WarmeDrankDecorator
    {
        private WarmeDrank _warmeDrank;
        public Melk(WarmeDrank aWarmeDrank)
        {
            _warmeDrank = aWarmeDrank;
        }
        public override string GetDescription()
        {
            return _warmeDrank.GetDescription() + " ,melk";
        }
    }
    public class Room : WarmeDrankDecorator
    {
        private WarmeDrank _warmeDrank;
        public Room(WarmeDrank aWarmeDrank)
        {
            _warmeDrank = aWarmeDrank;
        }
        public override string GetDescription()
        {
            return _warmeDrank.GetDescription() + " ,room";
        }
    }
    public class WarmeDrank
    {
        protected string description = "onbekende drank";
        List<WarmeDrankDecorator> extras = new List<WarmeDrankDecorator>();
        public virtual string GetDescription()
        {
            string tmp = description;
            foreach (WarmeDrankDecorator item in extras)
            {
                tmp += " with " + item.GetDescription();
            }
            return tmp;
        }
        public void AddMelk(WarmeDrankDecorator aWarmeDrankDecorator)
        {
            extras.Add(new Melk(aWarmeDrankDecorator));
        }
        public void AddRoom(WarmeDrankDecorator aWarmeDrankDecorator)
        {
            extras.Add(new Room(aWarmeDrankDecorator));
        }
    }
}
