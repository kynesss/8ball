using Medicine;
using UnityEngine;

namespace Common
{
    [Register.Single]
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public bool MobileModeOn { get; private set; }
    }
}
