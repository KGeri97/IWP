using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AssemblyNodeUI : BaseNodeUI
{
    public TMP_Text assemblyInformationText;
    public Image assemblyProductIcon;
    public List<Image> assemblyAttachIcon = new List<Image>();
    
    public void UpdateAssemblyText(string productType, string productionAmount)
    {
        assemblyInformationText.text = $"The assembly is producing: {productType}." +
                    $"{productionAmount}s per month";
    }

    public void UpdateAssemblyImage(Image productIcon)
    {
        assemblyProductIcon = productIcon;
    }

    public void UpdateAssemblyAttachIcon(Image nodeIcon, int attachNum)
    {
        assemblyAttachIcon[attachNum] = nodeIcon;
    }
}
