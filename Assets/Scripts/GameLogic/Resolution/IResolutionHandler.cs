namespace ESG.RockPaperScissors
{
    // Requires method(s) that allow a class to receive tabulated ResolutionData
    // from any implementation of IResolutionService.
    public interface IResolutionHandler
    {
        public void HandleResolutionData(ResolutionData resolutionData);
    }
}