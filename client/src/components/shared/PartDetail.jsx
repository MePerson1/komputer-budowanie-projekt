import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import axios from "axios";
import ReturnButton from "./ReturnButton";
import PartPrices from "./PartPrices";
import { PartsSpecifications } from "./PartsSepcifications";

const PartDetail = () => {
  const location = useLocation();
  const [part, setPart] = useState(null);
  const [specifications, setSpecifications] = useState(null);
  const { id } = useParams();
  const partType = location.pathname
    .replace(`/${id}`, "")
    .replace("/", "")
    .replace("parts", "")
    .replace("/", "");

  useEffect(() => {
    if (id) {
      getPart(partType, id);
    }
  }, [id, partType]);

  async function getPart(partType, id) {
    try {
      const response = await axios.get(
        `http://localhost:5198/api/${partType}/${id}`
      );
      setPart(response.data);
      transformSpecifications(response.data, partType);
    } catch (error) {
      console.log(error);
    }
  }

  function transformSpecifications(part, partType) {
    switch (partType) {
      case "cpu":
        setSpecifications(cpuSpecifications(part));
        break;
      case "cpu-cooling":
        setSpecifications(cpuCoolingSpecifications(part));
        break;
      case "water-cooling":
        setSpecifications(waterCoolingSpecifications(part));
        break;
      case "motherboard":
        setSpecifications(motherboardSpecifications(part));
        break;
      case "graphic-card":
        setSpecifications(gpuSpecifications(part));
        break;
      case "ram":
        setSpecifications(ramSpecifications(part));
        break;
      case "storage":
        setSpecifications(storageSpecifications(part));
        break;
      case "power-supply":
        setSpecifications(powerSupplySpecifications(part));
        break;
      case "case":
        setSpecifications(caseSpecifications(part));
        break;
      default:
        break;
    }
  }

  function cpuSpecifications(part) {
    var cpuSpecificationValue = [
      { name: "Linia", value: part.line },
      {
        name: "Zawiera chłodzenie",
        value: part.hasIncludedCooling ? "Tak" : "Nie",
      },
      { name: "Typ gniazda", value: part.socketType },
      { name: "Ilość rdzeni", value: part.numberOfCores },
      { name: "Ilość wątków", value: part.numberOfThreads },
      {
        name: "Podstawowa częstotliwość procesora (GHz)",
        value: part.processorBaseFrequencyGHz,
      },
      {
        name: "Maksymalna częstotliwość Turbo (GHz)",
        value: part.maxTurboFrequencyGHz,
      },
      { name: "Zintegrowana grafika", value: part.integratedGraphics },
      {
        name: "Odblokowany mnożnik",
        value: part.hasUnlockedMultiplier ? "Tak" : "Nie",
      },
      { name: "Architektura", value: part.architecture },
      { name: "Proces technologiczny", value: part.manufacturingProcess },
      {
        name: "Mikroarchitektura procesora",
        value: part.processorMicroarchitecture,
      },
      { name: "TDP (W)", value: part.tdPinW },
      {
        name: "Maksymalna temperatura pracy (°C)",
        value: part.maxOperatingTempC,
      },
      { name: "Pamięć cache L1", value: part.l1Cache },
      { name: "Pamięć cache L2", value: part.l2Cache },
      { name: "Pamięć cache L3", value: part.l3Cache },
      {
        name: "Dodatkowe wyposażenie",
        value: part.addedEquipment ? part.addedEquipment : "brak",
      },
    ];
    return cpuSpecificationValue;
  }
  function cpuCoolingSpecifications(part) {
    var cpuCoolingSpecificationValue = [
      { name: "Typ montażu", value: part.mountingType },
      { name: "Kolor", value: part.colorElement },
      { name: "Wysokość (mm)", value: part.heightMM },
      { name: "Szerokość (mm)", value: part.widthMM },
      { name: "Głębokość (mm)", value: part.depthMM },
      { name: "Waga (gramy)", value: part.weightGrams },
      { name: "Gniazdo procesora", value: part.processorSocket },
      { name: "Maksymalne TDP (W)", value: part.maxTDPinW },
      { name: "Materiał podstawy", value: part.baseMaterial },
      { name: "Oświetlenie", value: part.hasLighting ? "Tak" : "Nie" },
      { name: "Liczba rurek cieplnych", value: part.heatPipesCount },
      { name: "Liczba wentylatorów", value: part.fanCount },
      { name: "Średnica wentylatora (mm)", value: part.fanDiameterMM },
    ];
    return cpuCoolingSpecificationValue;
  }
  function waterCoolingSpecifications(part) {
    var waterCoolingSpecificationValue = [
      { name: "Kompatybilność z Intel", value: part.intelCompatibility },
      { name: "Kompatybilność z AMD", value: part.amdCompatibility },
      { name: "Waga (gramy)", value: part.weightG },
      {
        name: "Wymiary radiatora (mm)",
        value: `${part.radiatorSizeMM} x ${part.radiatorLengthMM} x ${part.radiatorWidthMM}`,
      },
      { name: "Wysokość radiatora (mm)", value: part.radiatorHeightMM },
      { name: "Liczba wentylatorów", value: part.fanCount },
      { name: "Średnica wentylatora (mm)", value: part.fanDiameterMM },
      {
        name: "Maksymalna prędkość obrotowa wentylatora (RPM)",
        value: part.maxFanSpeedRPM,
      },
      { name: "Sterowanie PWM", value: part.hasPWMControl ? "Tak" : "Nie" },
      {
        name: "Maksymalny przepływ powietrza (CFM)",
        value: part.maxAirflowCFM,
      },
      { name: "Maksymalny poziom hałasu (dBA)", value: part.maxNoiseLevelDBa },
      { name: "Złącze wentylatora", value: part.fanConnector },
      { name: "Złącze pompy", value: part.pumpConnector },
    ];
    return waterCoolingSpecificationValue;
  }
  function motherboardSpecifications(part) {
    var motherboardSpecificationValue = [
      { name: "Standard płyty", value: part.boardStandard },
      { name: "Szerokość (mm)", value: part.widthMM },
      { name: "Głębokość (mm)", value: part.depthMM },
      { name: "Chipset", value: part.chipset },
      { name: "Gniazdo CPU", value: part.cpuSocket },
      { name: "Obsługiwane procesory", value: part.supportedProcessors },
      { name: "Kontroler RAID", value: part.raidController },
      { name: "Standard pamięci", value: part.memoryStandard },
      { name: "Typ złącza pamięci", value: part.memoryConnectorType },
      { name: "Ilość slotów pamięci", value: part.memorySlotsCount },
      {
        name: "Obsługiwane częstotliwości pamięci",
        value: part.supportedMemoryFreq,
      },
      { name: "Maksymalna ilość pamięci (GB)", value: part.maxMemoryGB },
      { name: "Architektura kanałów pamięci", value: part.channelArchitecture },
      {
        name: "Obsługa zintegrowanej grafiki",
        value: part.hasIntegratedGraphicsSupport ? "Tak" : "Nie",
      },
      { name: "Chipset graficzny", value: part.graphicsChipset },
      { name: "Chipset dźwiękowy", value: part.soundChipset },
      { name: "Kanały audio", value: part.audioChannels },
      {
        name: "Zintegrowana karta sieciowa",
        value: part.integratedNetworkCard,
      },
      { name: "Chipset sieciowy", value: part.networkChipset },
      {
        name: "Sloty rozszerzeń",
        value: part.expansionSlots,
      },
      { name: "Złącza dysków", value: part.driveConnectors },
      {
        name: "Złącza wewnętrzne",
        value: part.internalConnectors,
      },
      {
        name: "Złącza na tylnym panelu",
        value: part.rearPanelConnectors,
      },
      {
        name: "Dołączone akcesoria",
        value: part.includedAccessories,
      },
    ];

    return motherboardSpecificationValue;
  }
  function gpuSpecifications(part) {
    var gpuSpecificationValue = [
      { name: "Producent chipsetu", value: part.chipsetProducer },
      { name: "Typ chipsetu", value: part.chipsetType },
      { name: "Taktowanie rdzenia (MHz)", value: part.coreClockMHz },
      { name: "Taktowanie boost (MHz)", value: part.boostClockMHz },
      {
        name: "Liczba procesorów strumieniowych",
        value: part.streamProcessors,
      },
      { name: "Jednostki ROP", value: part.ropUnits },
      { name: "Jednostki teksturujące", value: part.textureUnits },
      { name: "Rdzenie RT", value: part.rtCores },
      { name: "Rdzenie Tensor", value: part.tensorCores },
      { name: "Wsparcie DLSS3", value: part.hasDLSS3Support ? "Tak" : "Nie" },
      { name: "Typ złącza", value: part.connectorType },
      { name: "Długość karty (mm)", value: part.cardLengthMM },
      { name: "Maksymalna rozdzielczość", value: part.resolution },
      {
        name: "Zalecana moc zasilacza (W)",
        value: part.recommendedPSUCapacityW,
      },
      { name: "Oświetlenie LED", value: part.hasLEDLighting ? "Tak" : "Nie" },
      { name: "Pamięć karty (GB)", value: part.memorySizeGB },
      { name: "Typ pamięci", value: part.memoryType },
      { name: "Szerokość szyny pamięci (bit)", value: part.memoryBusWidthBits },
      { name: "Taktowanie pamięci (MHz)", value: part.memoryClockMHz },
      { name: "Typ chłodzenia", value: part.coolingType },
      { name: "Liczba wentylatorów", value: part.fanCount },
      { name: "Port D-Sub", value: part.dSub },
      { name: "Liczba portów DisplayPort", value: part.displayPortCount },
      { name: "Liczba portów mini DisplayPort", value: part.miniDisplayPort },
      { name: "Port DVI", value: part.dvi },
      { name: "Port HDMI", value: part.hdmi },
      { name: "Port USB-C", value: part.usbc },
      { name: "Złącza zasilania", value: part.powerConnectors },
    ];

    return gpuSpecificationValue;
  }
  function ramSpecifications(part) {
    var ramSpecificationValue = [
      { name: "Typ pinu", value: part.pinType },
      { name: "Typ pamięci", value: part.memoryType },
      { name: "Niski profil", value: part.lowProfile ? "Tak" : "Nie" },
      { name: "Rodzaj chłodzenia", value: part.cooling },
      { name: "Pojemność (GB)", value: part.capacityGB },
      { name: "Ilość modułów", value: part.moduleCount },
      { name: "Częstotliwość (MHz)", value: part.frequencyMHz },
      { name: "Opóźnienie CL", value: part.latencyCL },
      { name: "Napięcie (V)", value: part.voltageV },
      { name: "Profil podkręcania", value: part.overclockingProfile },
      { name: "Kolor", value: part.color },
      { name: "Oświetlenie", value: part.hasLighting ? "Tak" : "Nie" },
    ];

    return ramSpecificationValue;
  }
  function storageSpecifications(part) {
    var storageSpecificationValue;
    if (part.type === "SSD") {
      storageSpecificationValue = [
        { name: "Typ", value: part.type },
        { name: "Model", value: part.model },
        { name: "Forma", value: part.formFactor },
        { name: "Pojemność", value: part.capacity },
        { name: "Interfejs", value: part.interface },
        { name: "Grubość (mm)", value: part.thiccnessMM },
        { name: "Radiator", value: part.radiator ? "Tak" : "Nie" },
        { name: "Typ chipów pamięci", value: part.memoryChipType },
        { name: "Prędkość odczytu (MB/s)", value: part.readSpeedMBs },
        { name: "Prędkość zapisu (MB/s)", value: part.writeSpeedMBs },
        {
          name: "Ilość losowych odczytów na sekundę",
          value: part.readRandomIOPS,
        },
        {
          name: "Ilość losowych zapisów na sekundę",
          value: part.writeRandomIOPS,
        },
        { name: "Żywotność", value: part.longevity },
        { name: "TBW (Total Bytes Written)", value: part.tbw },
        { name: "Klucz", value: part.key },
        {
          name: "Szyfrowanie sprzętowe",
          value: part.hardwareEncryption ? "Tak" : "Nie",
        },
      ];
    } else {
      storageSpecificationValue = [
        { name: "Typ", value: part.type },
        { name: "Model", value: part.model },
        { name: "Forma", value: part.formFactor },
        { name: "Pojemność", value: part.capacity },
        { name: "Interfejs", value: part.interface },
        { name: "Grubość (mm)", value: part.thiccnessMM },
        { name: "Pamięć cache", value: part.cacheMemory },
        { name: "Poziom hałasu (DB)", value: part.noiseLevelDB },
        { name: "Prędkość obrotowa (RPM)", value: part.rotatingSpeedRPM },
        { name: "Waga (gramy)", value: part.weightG },
      ];
    }

    return storageSpecificationValue;
  }
  function powerSupplySpecifications(part) {
    var powerSupplySpecificationValue = [
      { name: "Forma", value: part.formFactor },
      { name: "Moc (W)", value: part.powerW },
      { name: "Certyfikat", value: part.certificate },
      { name: "Korekcja czynnika mocy", value: part.powerFactorCorrection },
      { name: "Ocena efektywności", value: part.efficiencyRating },
      {
        name: "Chłodzenie",
        value: `${part.cooling} - Wentylator ${part.fanDiameterMM}mm`,
      },
      { name: "Zabezpieczenia", value: part.security },
      { name: "Modularność kabli", value: part.modularCabling },
      { name: "Złącze ATX 24-pin + 20+4", value: part.atX24Pin_20Plus4 },
      { name: "Złącza PCI-E 8-pin (6+2)", value: part.pciE8Pin_6Plus2 },
      { name: "Złącza PCI-E 16-pin", value: part.pciE16Pin },
      { name: "Złącza PCI-E 8-pin", value: part.pciE8Pin },
      { name: "Złącza PCI-E 6-pin", value: part.pciE6Pin },
      { name: "Złącza CPU 8-pin (4+4)", value: part.cpU8Pin_4Plus4 },
      { name: "Złącza CPU 8-pin", value: part.cpU8Pin },
      { name: "Złącza CPU 4-pin", value: part.cpU4Pin },
      { name: "Złącza SATA", value: part.sata },
      { name: "Złącza Molex", value: part.molex },
      { name: "Wysokość (mm)", value: part.heightMM },
      { name: "Szerokość (mm)", value: part.widthMM },
      { name: "Głębokość (mm)", value: part.depthMM },
      { name: "Oświetlenie", value: part.hasLighting ? "Tak" : "Nie" },
    ];

    return powerSupplySpecificationValue;
  }
  function caseSpecifications(part) {
    var caseSpecificationValue = [
      { name: "Kolor", value: part.color },
      { name: "Oświetlenie", value: part.hasLightning ? "Tak" : "Nie" },
      { name: "Wysokość (cm)", value: part.heightCM },
      { name: "Długość (cm)", value: part.lengthCM },
      { name: "Szerokość (cm)", value: part.widthCM },
      { name: "Typ obudowy", value: part.caseType },
      { name: "Kompatybilność płyt głównych", value: part.compatibility },
      { name: "Okno", value: part.hasWindow ? "Tak" : "Nie" },
      { name: "Wyciszenie", value: part.isMuted ? "Tak" : "Nie" },
      {
        name: "Maksymalna długość karty graficznej (cm)",
        value: part.maxGPULengthCM,
      },
      {
        name: "Maksymalna wysokość systemu chłodzenia (cm)",
        value: part.maxCoolingSystemHeightCM,
      },
      { name: "Liczba portów USB 2.0", value: part.usbTwoCount },
      { name: "Liczba portów USB 3.0", value: part.usbThreeCount },
      { name: "Liczba portów USB 3.1", value: part.usbThreePointOneCount },
      { name: "Liczba portów USB 3.2", value: part.usbThreePointTwoCount },
      { name: "Liczba portów USB typu C", value: part.usbTypeCCount },
      {
        name: "Liczba portów USB do szybkiego ładowania",
        value: part.usbTurboChargingCount,
      },
      {
        name: "Czytnik kart pamięci",
        value: part.hasMemoryCardReader ? "Tak" : "Nie",
      },
      { name: "Port audio", value: part.hasAudioPort ? "Tak" : "Nie" },
      {
        name: "Port mikrofonowy",
        value: part.hasMicrophonePort ? "Tak" : "Nie",
      },
      {
        name: "Wewnętrzne zatoki 2.5 cala",
        value: part.internalBaysTwoPointFiveInch,
      },
      {
        name: "Wewnętrzne zatoki 3.5 cala",
        value: part.internalBaysThreePointFiveInch,
      },
      {
        name: "Zewnętrzne zatoki 3.5 cala",
        value: part.externalBaysThreePointFiveInch,
      },
      {
        name: "Zewnętrzne zatoki 5.25 cala",
        value: part.externalBaysFivePointTwoFiveInch,
      },
      { name: "Sloty rozszerzeń", value: part.expansionSlots },
      { name: "Panel przedni", value: part.panelFront },
      { name: "Panel boczny", value: part.panelSide },
      { name: "Panel górny", value: part.panelTop },
    ];

    return caseSpecificationValue;
  }

  return (
    <div className="">
      <ReturnButton />

      {part !== null ? (
        <div className="flex flex-col">
          <div className="bg-base-200 bg-opacity-30 justify-center items-start flex">
            <div className="w-32 h-32 border justify-center text-center flex">
              Zdjęcie
            </div>
            <div className="pl-5">
              <div className="flex">
                <p>Nazwa: </p>
                <p className="pl-1 font-bold">{part.name}</p>
              </div>
              <div className="flex">
                <p>Producent: </p>
                <p className="pl-1 font-bold"> {part.producer}</p>
              </div>
              <div className="flex ">
                <p className="flex items-center justify-center">Cena: </p>
                <PartPrices prices={part.prices} />
              </div>

              <div className="flex ">
                <p className="flex items-center justify-center">
                  Kod producenta:
                </p>
                <p className="pl-1 font-bold">{part.producerCode}</p>
              </div>
            </div>
          </div>

          <div>
            <p className="font-bold text-4xl bg-base-300">Specyfikacje</p>
            <PartsSpecifications specifications={specifications} />
          </div>
        </div>
      ) : (
        <div className="flex justify-center content-center  animate-bounce p-5">
          <span className="loading loading-spinner loading-lg"></span>
          <p className="content-cent p-5 text-3xl">Ładowanie</p>
        </div>
      )}
    </div>
  );
};

export default PartDetail;
