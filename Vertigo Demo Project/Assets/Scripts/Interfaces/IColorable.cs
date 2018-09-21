using UnityEngine;

public interface IColorable {

    int myColorIndex { get; set; }
    void changeColor(int index);
}
