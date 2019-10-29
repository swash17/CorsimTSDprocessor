namespace TSI_and_TS0_FileFormats
{
    public enum MessageNames
    {
        LG_Complete_GP = 3003,
        LG_Data_GP = 3001
    }
    public enum RequestTypes
    {
        DR_TI_LINK = 13000,
        DR_TS_INCIDENT = 14400,
        DR_TS_RAMPMETER = 14300,
        DR_TS_SIGNAL = 14200,
        DR_TS_VEHICLE = 14000
    }
    public enum ClassIDs
    {
        Incident = 13000,
        Link = 18000,
        LinkMOE = 19000,
        //Vehicle = 33000
        Vehicle = 34000 // File description document says 33000, but the sample files use 34000
    }
    public enum ActionIDs
    {
        CREATE = 0,
        SEARCH = 2,
        UPDATE = 1
    }
    public enum FleetTypes
    {
        Auto = 0,
        truck = 1,
        carpool = 2,
        bus = 3
    }
    public enum VehicleTurnCodes
    {
        left = 0,
        through = 1,
        right = 2,
        leftDiagonal = 3,
        rightDiagonal = 4,
        sourceEmission = 5
    }
    public enum QueueStatuses
    {
        vehicleIsCurrentlyNotInQueue = 0,
        vehicleIsCurrentlyInQueue = 1
    }
    public enum LaneChangeStatuses
    {
        vehicleDoesNotWantToChangeLanes = 0,
        vehicleWantsToChangeLanes = 1
    }
    public enum SignalCodes
    {
        red = 0,
        yellow = 1,
        protectedGreen = 2,
        green = 3,
        none = 4
    }
    public enum IncidentType
    {
        unknown = 0,
        freeway = 1,
        longTerm = 2,
        parking = 3,
        shortTerm = 4
    }
    public enum CorsimModelType
    {
        NETSIM = 3,
        FRESIM = 8
    }
    public enum IncidentState
    {
        notInProgress = 0,
        inProgress = 1
    }
    public enum AttributeIDs
    {
        IN_AffectedLaneSLT = 13501,
        IN_Duration = 13106,
        IN_IncidentId = 13100,
        IN_IncidentLength = 13104,
        IN_IncidentPosition = 13103,
        IN_IncidentReactionPointPosition = 13107,
        IN_IncidentState = 13500,
        IN_IncidentType = 13102,
        IN_LinkId = 13101,
        IN_ModelType = 13109,
        IN_OccurrenceTime = 13105,
        IN_RubberneckFactor = 13108,
        LK_SignalState = 18500,
        LM_BusDelayTotal = 19401,
        LM_BusDelayTotal_Cum = 19701,
        LM_BusesThatStopped = 19407,
        LM_BusesThatStopped_Cum = 19707,
        LM_BusMoveTimePerTravelTimeRatio = 19402,
        LM_BusMoveTimePerTravelTimeRatio_Cum = 19702,
        LM_BusPersonTrips = 19403,
        LM_BusPersonTrips_Cum = 19703,
        LM_BusSpeedAverage = 19404,
        LM_BusSpeedAverage_Cum = 19704,
        LM_BusTravelTimeTotal = 19405,
        LM_BusTravelTimeTotal_Cum = 19705,
        LM_BusTrips = 19406,
        LM_BusTrips_Cum = 19706,
        LM_ContentAverage = 19408,
        LM_ContentAverage_Cum = 19708,
        LM_ContentCurrent = 19409,
        LM_ContentCurrent_Cum = 19709,
        LM_DelayControlPerVehicle = 19410,
        LM_DelayControlPerVehicle_Cum = 19710,
        LM_DelayControlPerVehicleLeft = 19411,
        LM_DelayControlPerVehicleLeft_Cum = 19711,
        LM_DelayControlPerVehicleRight = 19412,
        LM_DelayControlPerVehicleRight_Cum = 19712,
        LM_DelayControlPerVehicleThrough = 19413,
        LM_DelayControlPerVehicleThrough_Cum = 19713,
        LM_DelayControlTotal = 19414,
        LM_DelayControlTotal_Cum = 19714,
        LM_DelayControlTotalLeft = 19415,
        LM_DelayControlTotalLeft_Cum = 19715,
        LM_DelayControlTotalRight = 19416,
        LM_DelayControlTotalRight_Cum = 19716,
        LM_DelayControlTotalThrough = 19417,
        LM_DelayControlTotalThrough_Cum = 19717,
        LM_DelayQueuePerVehicle = 19418,
        LM_DelayQueuePerVehicle_Cum = 19718,
        LM_DelayQueueTotal = 19419,
        LM_DelayQueueTotal_Cum = 19719,
        LM_DelayQueueTotalLeft = 19420,
        LM_DelayQueueTotalLeft_Cum = 19720,
        LM_DelayQueueTotalRight = 19421,
        LM_DelayQueueTotalRight_Cum = 19721,
        LM_DelayQueueTotalThrough = 19422,
        LM_DelayQueueTotalThrough_Cum = 19722,
        LM_DelayStopPerVehicle = 19423,
        LM_DelayStopPerVehicle_Cum = 19723,
        LM_DelayStopTotal = 19424,
        LM_DelayStopTotal_Cum = 19724,
        LM_DelayStopTotalLeft = 19425,
        LM_DelayStopTotalLeft_Cum = 19725,
        LM_DelayStopTotalRight = 19426,
        LM_DelayStopTotalRight_Cum = 19726,
        LM_DelayStopTotalThrough = 19427,
        LM_DelayStopTotalThrough_Cum = 19727,
        LM_DelayTravelPerVehicle = 19428,
        LM_DelayTravelPerVehicle_Cum = 19728,
        LM_DelayTravelPerVehicleLeft = 19429,
        LM_DelayTravelPerVehicleLeft_Cum = 19729,
        LM_DelayTravelPerVehicleRight = 19430,
        LM_DelayTravelPerVehicleRight_Cum = 19730,
        LM_DelayTravelPerVehicleThrough = 19431,
        LM_DelayTravelPerVehicleThrough_Cum = 19731,
        LM_DelayTravelTotal = 19432,
        LM_DelayTravelTotal_Cum = 19732,
        LM_DelayTravelTotalLeft = 19433,
        LM_DelayTravelTotalLeft_Cum = 19733,
        LM_DelayTravelTotalRight = 19434,
        LM_DelayTravelTotalRight_Cum = 19734,
        LM_DelayTravelTotalThrough = 19435,
        LM_DelayTravelTotalThrough_Cum = 19735,
        LM_DensityPerLane = 19436,
        LM_DensityPerLane_Cum = 19736,
        LM_EmissionsRateCO = 19437,
        LM_EmissionsRateCO_Cum = 19737,
        LM_EmissionsRateHC = 19438,
        LM_EmissionsRateHC_Cum = 19738,
        LM_EmissionsRateNOx = 19439,
        LM_EmissionsRateNOx_Cum = 19739,
        LM_EmissionsTotalCO = 19440,
        LM_EmissionsTotalCO_Cum = 19740,
        LM_EmissionsTotalHC = 19441,
        LM_EmissionsTotalHC_Cum = 19741,
        LM_EmissionsTotalNOx = 19442,
        LM_EmissionsTotalNOx_Cum = 19742,
        LM_FuelConsumptionTotal = 19443,
        LM_FuelConsumptionTotal_Cum = 19743,
        LM_FuelConsumptionTotalAutos = 19444,
        LM_FuelConsumptionTotalAutos_Cum = 19744,
        LM_FuelConsumptionTotalBuses = 19445,
        LM_FuelConsumptionTotalBuses_Cum = 19745,
        LM_FuelConsumptionTotalCarpools = 19446,
        LM_FuelConsumptionTotalCarpools_Cum = 19746,
        LM_FuelConsumptionTotalTrucks = 19447,
        LM_FuelConsumptionTotalTrucks_Cum = 19747,
        LM_LaneChangesTotal = 19448,
        LM_LaneChangesTotal_Cum = 19748,
        LM_MoveTimePerTravelTimeRatio = 19449,
        LM_MoveTimePerTravelTimeRatio_Cum = 19749,
        LM_MoveTimePerTravelTimeRatioLeft = 19450,
        LM_MoveTimePerTravelTimeRatioLeft_Cum = 19750,
        LM_MoveTimePerTravelTimeRatioRight = 19451,
        LM_MoveTimePerTravelTimeRatioRight_Cum = 19751,
        LM_MoveTimePerTravelTimeRatioThrough = 19452,
        LM_MoveTimePerTravelTimeRatioThrough_Cum = 19752,
        LM_MoveTimeTotal = 19453,
        LM_MoveTimeTotal_Cum = 19753,
        LM_MoveTimeTotalLeft = 19454,
        LM_MoveTimeTotalLeft_Cum = 19754,
        LM_MoveTimeTotalRight = 19455,
        LM_MoveTimeTotalRight_Cum = 19755,
        LM_MoveTimeTotalThrough = 19456,
        LM_MoveTimeTotalThrough_Cum = 19756,
        LM_PersonDelayTotal = 19457,
        LM_PersonDelayTotal_Cum = 19757,
        LM_PersonTripsTotal = 19458,
        LM_PersonTripsTotal_Cum = 19758,
        LM_PhaseFailuresTotal = 19459,
        LM_PhaseFailuresTotal_Cum = 19759,
        LM_QueueAverageNumberVehiclesSLT = 19460,
        LM_QueueAverageNumberVehiclesSLT_Cum = 19760,
        LM_QueueMaximumNumberVehiclesSLT = 19461,
        LM_QueueMaximumNumberVehiclesSLT_Cum = 19761,
        LM_SpeedAverage = 19462,
        LM_SpeedAverage_Cum = 19762,
        LM_SpeedAverageLeft = 19463,
        LM_SpeedAverageLeft_Cum = 19763,
        LM_SpeedAverageRight = 19464,
        LM_SpeedAverageRight_Cum = 19764,
        LM_SpeedAverageThrough = 19465,
        LM_SpeedAverageThrough_Cum = 19765,
        LM_StoppedVehicles = 19466,
        LM_StoppedVehicles_Cum = 19766,
        LM_StoppedVehiclesPercent = 19467,
        LM_StoppedVehiclesPercent_Cum = 19767,
        LM_StoragePercent = 19468,
        LM_StoragePercent_Cum = 19768,
        LM_TimeInterval = 19400,
        LM_TimeInterval_Cum = 19700,
        LM_TravelDistanceTotal = 19469,
        LM_TravelDistanceTotal_Cum = 19769,
        LM_TravelDistanceTotalLeft = 19470,
        LM_TravelDistanceTotalLeft_Cum = 19770,
        LM_TravelDistanceTotalRight = 19471,
        LM_TravelDistanceTotalRight_Cum = 19771,
        LM_TravelDistanceTotalThrough = 19472,
        LM_TravelDistanceTotalThrough_Cum = 19772,
        LM_TravelTimePerVehicle = 19473,
        LM_TravelTimePerVehicle_Cum = 19773,
        LM_TravelTimePerVehicleLeft = 19474,
        LM_TravelTimePerVehicleLeft_Cum = 19774,
        LM_TravelTimePerVehicleRight = 19475,
        LM_TravelTimePerVehicleRight_Cum = 19775,
        LM_TravelTimePerVehicleThrough = 19476,
        LM_TravelTimePerVehicleThrough_Cum = 19776,
        LM_TravelTimeTotal = 19477,
        LM_TravelTimeTotal_Cum = 19777,
        LM_TravelTimeTotalLeft = 19478,
        LM_TravelTimeTotalLeft_Cum = 19778,
        LM_TravelTimeTotalRight = 19479,
        LM_TravelTimeTotalRight_Cum = 19779,
        LM_TravelTimeTotalThrough = 19480,
        LM_TravelTimeTotalThrough_Cum = 19780,
        LM_Trips = 19481,
        LM_Trips_Cum = 19781,
        LM_TripsLeft = 19482,
        LM_TripsLeft_Cum = 19782,
        LM_TripsRight = 19483,
        LM_TripsRight_Cum = 19783,
        LM_TripsThrough = 19484,
        LM_TripsThrough_Cum = 19784,
        LM_VehiclesDischarged = 19485,
        LM_VehiclesDischarged_Cum = 19785,
        LM_VehiclesDischargedLeft = 19486,
        LM_VehiclesDischargedLeft_Cum = 19786,
        LM_VehiclesDischargedRight = 19487,
        LM_VehiclesDischargedRight_Cum = 19787,
        LM_VehiclesDischargedThrough = 19488,
        LM_VehiclesDischargedThrough_Cum = 19788,
        LM_Volume = 19489,
        LM_Volume_Cum = 19789,
        LM_VolumePerLane = 19490,
        LM_VolumePerLane_Cum = 19790,
        V_InputAndAnimate = 33500
    }
}
