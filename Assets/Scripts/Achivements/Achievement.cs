using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement {
    public string title;
    public string description;
    public Predicate<object> requirement;
    public Sprite img;
    public bool achieved;

    public Achievement(Sprite img, string title, string description, Predicate<object> requirement)
    {
        this.img = img;
        this.title = title;
        this.description = description;
        this.requirement = requirement;
    }

    public bool UpdateCompletion()
    {
        if (achieved) return false;

        if (areRequirementsMet()) {
            Debug.Log($"{title}: {description}");
            achieved = true;
            return true;
        }
        return false;
    }

    public bool areRequirementsMet()
    {
        return requirement.Invoke(null);
    }
}