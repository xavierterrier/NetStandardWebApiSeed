namespace WebAPIToolkit.Model
{
    public class Enums
    {
        public enum Practice
        {
            mobile,
            web,
            bi,
            ecm,
            security,
            methode,
            quality,
            innovation,
            support
        }

        public enum Status
        {
            ON_TIME,
            AT_RISK,
            LATE,
            NONE,
            ARCHIVED
        }

        public enum State
        {
            TO_DO,
            IN_PROGRESS,
            DONE,
            LATE
        }

    }
}