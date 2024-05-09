using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IHoverable
{
  bool IsHoverable();
  string GetHoverText();
  GameObject GetHoverUI();
}