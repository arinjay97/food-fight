using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy 
{
    int Health { get; set; }
    Transform hand { get; set; }
    GameObject foodItem { get; set; }
    Transform player { get; set; }
    GameObject throwable { get; set; }

}
