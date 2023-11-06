using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Services
{
    public class CompatibilityService : ICompatibilityService
    {
        protected KomBuildDbContext _context;
        public CompatibilityService(KomBuildDbContext context)
        {
            this._context = context;
        }

        public async Task<string> CompatibilityCheck(PcConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public async Task<Toast?> CpuCompatibilityCheck(PcConfiguration configuration)
        {

            Toast toast = new Toast();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }

            if (configuration.Motherboard != null)
            {
                //CheckSupportedProcessor
                if (!configuration.Motherboard.SupportedProcessors.Contains(configuration.Cpu.Line))
                {
                    toast.Problems.Add("Motherboard doesn't support this Cpu line!");
                }
                //CheckCPUSocket
                if (configuration.Motherboard.CPUSocket != configuration.Cpu.SocketType)
                {
                    toast.Problems.Add("Motherboard socket is different then cpu socket type!");
                }
            }

            return toast;
        }

        public async Task<Toast?> MemoryCompatibilityCheck(PcConfiguration configuration)
        {

            Toast toast = new Toast();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }

            if (configuration.Motherboard != null)
            {

            }

            return toast;
        }

        public Task<Toast?> RamCompatibilityCheck(PcConfiguration configuration)
        {

            Toast toast = new Toast();

            var ramCount = 0;

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return Task.FromResult<Toast?>(toast);
            }

            //MotherboardRamCheck
            if (configuration.Motherboard != null)
            {
                foreach (Ram ram in configuration.Rams)
                {
                    ramCount += ram.ModuleCount;
                    //CheckRamStandard
                    if (configuration.Motherboard.MemoryStandard != ram.MemoryType)
                    {
                        toast.Problems.Add("Motherboard doesn't support this memory standard! " + ram.Name);
                    }
                    //CheckMRamPinType
                    if (configuration.Motherboard.MemoryConnectorType != ram.PinType) //zmienić nazwe zmiennych może
                    {
                        toast.Problems.Add("Motherboard connector is different then ram pin type! " + ram.Name);
                    }
                }
                //CheckRamSlotCount
                if (configuration.Motherboard.MemorySlotsCount != ramCount)
                {
                    toast.Problems.Add("Motherboard don't have enough memory slots! Memory slots count: " + configuration.Motherboard.MemorySlotsCount);
                }

            }

            //CheckCoolingSize
            //tutaj sprawdzenie rozmiaru chłodzenia i czy ram jest lowprofile przy tym ale jeszcze nie ma cpucooling kinda
            if (configuration.CPU_Cooling != null)
            {

                foreach (Ram ram in configuration.Rams)
                {

                }
            }

            return Task.FromResult<Toast?>(toast);

        }

        public async Task<Toast?> CaseCompatibilityCheck(PcConfiguration configuration)
        {
            Toast toast = new Toast();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
                return toast;
            }

            if (configuration.Motherboard != null)
            {
                //Problem w tym ,że obsługiwane standardy w case są zapisywane naraz kilka i po przecinku!
                if (configuration.Motherboard.BoardStandard == "ATX")
                {
                    if (!configuration.Case.Compatibility.Contains(configuration.Motherboard.BoardStandard + ","))
                    {
                        toast.Problems.Add("Case is not compatibility with motherboard standard!");
                    }
                }
                else
                {
                    if (!configuration.Case.Compatibility.Contains(configuration.Motherboard.BoardStandard))
                    {
                        toast.Problems.Add("Case is not compatibility with motherboard standard!");
                    }
                }

            }

            if (configuration.PowerSupply != null)
            {
                if (configuration.Case.PowerSupply is null)
                {
                    //nie tak chyba xd
                    //if(configuration.PowerSupply.FormFactor != configuration.Case.PowerSupply)
                    //{

                    //}
                }
                else
                {
                    toast.Problems.Add("Case already contains power supply!");
                }

            }

            if (configuration.GraphicCard != null)
            {
                if (configuration.Case.MaxGPULengthCM < (configuration.GraphicCard.CardLengthMM * 10))
                {
                    toast.Problems.Add("Graphic Card is too long!");
                }
            }


            if (configuration.CPU_Cooling != null)
            {
                //w zaleznosci od jednostki height dla cpu cooling
                if (configuration.Case.MaxCoolingSystemHeightCM < configuration.CPU_Cooling.Height)
                {
                    toast.Problems.Add("Cpu Cooling is too high xd!");
                }
            }


            if (configuration.Memories != null)
            {
                foreach (var storage in configuration.Memories)
                {
                    if (storage.FormFactor == "3.5 cala")
                    {
                        if (configuration.Case.InternalBaysThreePointFiveInch == 0)
                        {
                            toast.Problems.Add("Case doesn't include internal bays for 3.5\" storage!");
                        }
                    }
                    if (storage.FormFactor == "2.5 cala")
                    {
                        if (configuration.Case.InternalBaysTwoPointFiveInch == 0)
                        {
                            toast.Problems.Add("Case doesn't include internal bays for 2.5\" storage!");
                        }

                    }
                }
            }

            return toast;


        }



    }
}

public class Toast
{
    public ICollection<string> Warnings { get; set; } = new List<string>();
    public ICollection<string> Problems { get; set; } = new List<string>();
}