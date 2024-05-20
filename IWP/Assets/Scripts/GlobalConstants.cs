using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalConstants {
    # region ManufacturingLine
    public static float BURST_SEND_DELAY { get; } = 0.5f;
    public static int LINE_QUALITY_DEPRECIATION_RATE = 1000; //Manufacturing line quality drops after every this many batches produced
    //public static int BATCH_QUALITY_DROP_CHANCE = 500000; //Quality of a batch has 1 in BATCH_QUALITY_DROP_CHANCE chance to be lowered
    public static int PRODUCT_QUALITY_DROP_CHANCE = 500000; //Quality of a product has 1 in PRODUCT_QUALITY_DROP_CHANCE chance to be lowered
    #endregion
}
