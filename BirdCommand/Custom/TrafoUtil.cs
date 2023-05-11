using Dalssoft.DiagramNet;
using System.Collections.Generic;

namespace BirdCommand.Custom
{
    public enum RuleType
    {
        MoveForward, TurnRight, TurnLeft, Turn180, NotSupportedYet
    }

    public enum TrafoProgress
    {
        Highlight = 0, Unhighlight = 1, Error = 2,
        Success = 3,
        Failure = 4,
        UpdateModel = 5
    }

    public class TrafoUtil
    {

        public static List<BaseElement> FindPreConditionElementsChemistry(List<BaseElement> allElements, BaseElement parentElement)
        {
            List<BaseElement> result = new List<BaseElement>();
            foreach (var element in allElements)
            {
                BaseElement casted = element as BaseElement;
                if (casted is IonCell)
                {
                    if (casted.Location.Y >= parentElement.Location.Y
                        && casted.Location.Y <= parentElement.Location.Y + parentElement.Size.Height
                        && casted.Location.Y + casted.Size.Height <= parentElement.Location.Y + parentElement.Size.Height
                        && casted.Location.X >= parentElement.Location.X
                        && casted.Location.X <= parentElement.Location.X + parentElement.Size.Width / 2
                        && casted.Location.X + casted.Size.Width <= parentElement.Location.X + parentElement.Size.Width / 2)
                    {
                        result.Add(casted);
                    }
                }
            }
            return result;
        }

        public static List<BaseElement> FindPostConditionElementsChemistry(List<BaseElement> allElements, BaseElement parentElement)
        {
            List<BaseElement> result = new List<BaseElement>();
            foreach (var element in allElements)
            {
                BaseElement casted = element as BaseElement;
                if (casted is IonCell)
                {
                    if (casted.Location.Y >= parentElement.Location.Y
                        && casted.Location.Y <= parentElement.Location.Y + parentElement.Size.Height
                        && casted.Location.Y + casted.Size.Height <= parentElement.Location.Y + parentElement.Size.Height
                        && casted.Location.X >= parentElement.Location.X + parentElement.Size.Width / 2
                        && casted.Location.X <= parentElement.Location.X + parentElement.Size.Width
                        && casted.Location.X + casted.Size.Width <= parentElement.Location.X + parentElement.Size.Width)
                    {
                        result.Add(casted);
                    }
                }
            }
            return result;
        }
    }
}
