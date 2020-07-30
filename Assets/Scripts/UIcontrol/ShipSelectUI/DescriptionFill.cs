using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 临时出包添加脚本

public class ShipInfo
{
    public string Name;
    public string Description;
    
    public int Fire;
    public int Alive;
    public int Exist;

    public string Weapon;
    public string WeaponDescription;

    public ShipInfo(string name, string description, string weapon, string weaponDescription, int fire, int alive, int exist)
    {
        Name = name;
        Description = description;
        Weapon = weapon;
        WeaponDescription = weaponDescription;
        Fire = fire;
        Alive = alive;
        Exist = exist;
    }
    
    public static ShipInfo Ship1 = new ShipInfo(
        "静默级：伐木工之子",
        "编号24601的帝国静默级战舰。\n“伐木工之子”是为了纪念那位从帝国监狱当中逃离的勇敢正直者而诞生的名字，他是这艘船的第一任非帝国军官舰长。在“伐木工之子”被病痛折磨死去之后，他托自己的女儿把这艘船送给了反抗军。在经过“伐木工之子”的数次改造之后，这艘静默级战舰与原本的模样截然不同，一些无法从非官方渠道获取的武器系统被拆除，这使得24601比一般的静默级战舰稍小一些。",
        "舰载磁轨炮",
        "中等频率向前方发射一枚低伤害弹丸\n实弹动能类的武器不需要高精度的设备，因而并不需要官方渠道就能够获得，这类舰载磁轨炮是不少星际海盗的首选装备——当然，是入不了眼的小海盗。",
        60,
        70,
        70
    );
    public static ShipInfo Ship2 = new ShipInfo(
        "静默级：无谓的牺牲",
        "编号已经模糊不清的帝国静默级战舰。\n“无谓的牺牲”是对他上一任主人的称呼。那是一位担任帝国军官的年轻人，他为了在抓捕反抗军的过程中不伤害到平民而选择撞上了一颗小行星，反抗军们将这艘战舰从残骸整备到了勉强能够使用。\n撞击损坏了这艘战舰的护甲发生器，在反抗军整备的过程中，护甲发生器所在的位置被更改成了武器挂载系统，不过如果有足够的资源的话，兼顾护甲发生器和武器挂载并不是问题。",
        "等离子鱼叉",
        "向前方快速发射夹角为15度的三束鱼叉形等离子束，其中中间一束体积和大小都比较高\n非官方渠道生产的等离子束武器，如同它的名字“鱼叉”一样，能够用三束等离子束轻而易举地洞穿想要逃离战场的漏网之鱼。当然，这需要你的能源充足。",
        80,
        70,
        40
        );
}

public class DescriptionFill : MonoBehaviour
{
    public Text shipName, description, weapon, weaponDescription;
    public NormalSliderCantControl fire, alive, exist;
    
    public void FillDescription(ShipInfo info)
    {
        shipName.text = info.Name;
        description.text = info.Description;
        weapon.text = info.Weapon;
        weaponDescription.text = info.WeaponDescription;
        
        fire.ValueTo(info.Fire/100.0f, 0.5f);
        alive.ValueTo(info.Alive/100.0f, 0.5f);
        exist.ValueTo(info.Exist/100.0f, 0.5f);
    }
}
