namespace DefaultNamespace
{
    public class AchievementModel
    {
        private readonly string _id;

        public AchievementModel(string id)
        {
            _id = id;
        }

        public string GetId()
        {
            return _id;
        }
    }
}