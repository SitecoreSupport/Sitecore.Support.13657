using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Abstractions;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.ItemProvider.SaveItem;
using Sitecore.Data.Fields;

namespace Sitecore.Support.Pipelines.ItemProvider.SaveItem
{
  public class EnsureVersionExists : Sitecore.Pipelines.ItemProvider.SaveItem.EnsureVersionExists
  {
    /// <summary>
    /// The item manager responsible for adding item version.
    /// </summary>

    public override void Process(SaveItemArgs args)
    {
      Assert.ArgumentNotNull(args, "args");
      Item item = args.Item;
      List<Field> fieldsToReset = new List<Field>();
      if (item.Versions.Count <= 0 && !this.IsPublishingOperation(item))
      {
        foreach (Field itemField in item.Fields)
        {
          if (itemField.Shared && itemField.GetValue(false, false) == null)
            fieldsToReset.Add(itemField);
        }
      }
      base.Process(args);
      if (fieldsToReset.Count > 0)

        foreach (var field in fieldsToReset)
        {
          field.Reset();
        }




    }

    public EnsureVersionExists(BaseItemManager itemManager) : base(itemManager)
    {
    }

  }
}