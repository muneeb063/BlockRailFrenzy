using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularManager : MonoBehaviour, SingularLinkHandler
{
    private void Awake()
    {
        SingularSDK.SetSingularLinkHandler(this);
    }
    void Start()
    {
        SingularSDK.SetCustomUserId("CustomerUserIdStringGoesHere");
    }
    public void OnSingularLinkResolved(SingularLinkParams linkParams)
    {
        Debug.Log("Singular == OnSingularLinkResolved");
        // The deep link destination, as configured in the Manage Links page
        string deeplink = linkParams.Deeplink;
        Debug.Log("Singular == DeepLink" + deeplink);

        // The passthrough parameters added to the link, if any.
        string passthrough = linkParams.Passthrough;
        Debug.Log("Singular == DeepLink" + passthrough);

        // Whether the link configured as a deferred deep link.
        bool isLinkDeferred = linkParams.IsDeferred;
        Debug.Log("Singular == DeepLink" + isLinkDeferred);
    }
    public void LogNonRevenueEvent(string value)
    {
        SingularSDK.Event(value);
    }
}
