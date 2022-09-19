using TMPro;

using UnityEngine;

namespace CubeDefender.Chip
{
    /// <summary>
    /// Базовый класс всех “фишек” на игровом поле. По сути является Viewer или Presenter, с хранением основных полей для изменения отображения или core-работы “фишки”
    /// </summary>
    public abstract class BaseChip : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _valueInfo;
        public int Hash => GetHashCode();

        public TextMeshPro ValueInfo => _valueInfo;
    }
}
