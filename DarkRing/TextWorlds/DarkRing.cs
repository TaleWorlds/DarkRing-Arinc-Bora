using System;

public class Game
{
    public void Start()
    {
        _changeStateInternal(new PrologueState());
    }
    public static Game Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Game();
            }
            return _instance;
        }
    }
    private static Game _instance;
    private Game() { }

    private State _state;
    internal void _changeStateInternal(State state)
    {
        _state = state;
        _state.PrintSituation();
    }

    private bool _gameStartCheck()
    {
        if (_state == null)
        {
            Console.WriteLine("You have to start the game first.");
            return false;
        }
        return true;
    }
    public void PrintSituation()
    {
        if (_gameStartCheck())
            _state.PrintSituation();
    }
    public void Proceed()
    {
        if (_gameStartCheck())
            _state.Proceed();
    }
    public void Strike()
    {
        if (_gameStartCheck())
            _state.Strike();
    }
    public void Parry()
    {
        if (_gameStartCheck())
            _state.Parry();
    }
    public void Dodge()
    {
        if (_gameStartCheck())
            _state.Dodge();
    }

    internal void _deathMessageInternal()
    {
        Console.WriteLine(
            "-------------------------------------------------------------------------\n" +
            "      \\\\    // //====\\\\  ||      ||     ||===\\\\  || ||==== ||===\\\\       \n" +
            "       \\\\  // ||      || ||      ||     ||    || || ||     ||    ||      \n" +
            "        \\\\//  ||      || ||      ||     ||    || || ||==== ||    ||      \n" +
            "         ||   ||      || ||      ||     ||    || || ||     ||    ||      \n" +
            "         ||    \\\\====//   \\\\====//      ||===//  || ||==== ||===//       \n" +
            "-------------------------------------------------------------------------"
        );
    }
}

internal abstract class State
{
    public Game GameContext
    {
        get;
    }
    public State()
    {
        GameContext = Game.Instance;
    }
    private void _illegalAction()
    {
        Console.WriteLine("You can't do that right now.");
    }
    public abstract void PrintSituation();
    public virtual void Proceed()
    {
        _illegalAction();
    }
    public virtual void Strike()
    {
        _illegalAction();
    }
    public virtual void Parry()
    {
        _illegalAction();
    }
    public virtual void Dodge()
    {
        _illegalAction();
    }
}

internal sealed class PrologueState : State
{
    public override void PrintSituation()
    {
        Console.WriteLine("Humans and orcs were living peacefully in the continent.\r\nThen the Orc Nation attacked. Rumors were that an entity called Golionerdh has crafted a divine artifact called the Dark Ring which gave immense power to its holder.\r\nUsing this power, he created an iron rule on the orcs and started an assault on the humans.\r\n\r\nYou are Kaladin. You and your wife Ezra were living in a border town, living a peaceful life. \n Then that night came. In one night, the orcs attacked your town and took control. Anyone who stood against them were killed without hesitation.\n After that, they separated the women and children from the men and performed a ritual to them. \nYou saw that the ritual pulled a ghastly essence from the women and children and absorbed them into a dark ring that Golionerdh wore.\r\n\r\nYou have become a slave. The men capable of holding a pickaxe were sent to the mines to work 18 hours a day.\n One night, a white shape with many eyes which looked very delicate appeared in your dreams. It identified itself as the Angel.\r\nAngel: We normally do not intervene in worldly manners. However, the Dark Ring is tampering with what the High One disallowed to be touched, the life essence. \n We have chosen you as the savior of humans and orcs. As long as you have your determination, we will be by your side and help you destroy the Dark Ring.\r\n\r\nYou wake up. You grab your pickaxe and walk towards the exit. The guard is looking at you menacingly as if he wants you to try something.\n Will you fight the guard? (Proceed)");

    }

    public sealed override void Proceed()
    {
        GameContext._changeStateInternal(new GrazgulFirstLightAttackState());
    }
}

internal class FirstBossFailState : State
{
    public sealed override void PrintSituation()
    {
        Game.Instance._deathMessageInternal();
        Console.WriteLine("\n You were dead just now. You remember the feeling of dying. But now you are alive again.\n You feel determination in your heart to kill Grazgul. Will you proceed?");
    }

    public sealed override void Proceed()
    {
        Game.Instance._changeStateInternal(new GrazgulFirstLightAttackState());
    }
}

internal class GrazgulFirstLightAttackState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("After seeing you come close, Grazgul puts his hand to his mace.");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You strike him with your pickaxe. He was faster than you and before your pickaxe hit, your head got torn. You are dead.");
        Game.Instance._changeStateInternal(new FirstBossFailState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You parry his mace with your pickaxe. You can feel he did not put his full power to the hit.");
        Game.Instance._changeStateInternal(new GrazgulHeavyAttackState());
    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You dodge his mace. He looks a bit surprised.");
        Game.Instance._changeStateInternal(new GrazgulHeavyAttackState());
    }
}

internal class GrazgulHeavyAttackState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Grazgul: Little man not bad.\nGrazgul pulls his mace back a bit to gain momentum");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You strike him with your pickaxe. You hit his leg, leaving a medium sized scratch");
        Game.Instance._changeStateInternal(new GrazgulSecondStageFootAttackState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You parry his mace with your pickaxe. However, this time the power of the blow was great and the mace squashed you like a bug. \n You are dead");
        Game.Instance._changeStateInternal(new FirstBossFailState());
    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You dodge his mace. You could see the trajectory of the mace from miles away.");
        Game.Instance._changeStateInternal(new GrazgulSecondLightAttackState());
    }
}

internal class GrazgulSecondLightAttackState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Grazgul: Little guy not bad.\nYou can see that Grazgul has something sharp in his other hand.");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You strike him with your pickaxe. He was much faster than you, and the dagger in his hand tore your artery. \n You blead to death in seconds.");
        Game.Instance._changeStateInternal(new FirstBossFailState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You parry his left hand dagger with the pickaxe.");
        Game.Instance._changeStateInternal(new GrazgulHeavyAttackState());
    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You were too slow to dodge. The left hand dagger slashed your body from side to side. \n You fell down to your knees. Grazgul held your head and tore it from your body.");
        Game.Instance._changeStateInternal(new FirstBossFailState());
    }
}

internal class GrazgulSecondStageFootAttackState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Grazgul: That tingled. Little man pay for tingle.\nGrazgul gets closer to the ground by retracting his knees.");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You strike him with your pickaxe. Grazgul jumped on top of you and squashed you to the ground. You have become a human pulp.");
        Game.Instance._changeStateInternal(new FirstBossFailState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You try to parry. There is nothing to parry. He jumped on top of you and his foot opened a foot shaped hole in your belly.");
        Game.Instance._changeStateInternal(new FirstBossFailState());
    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You anticipated him trying to jump on you. You dodge the fall by rolling right. Grazgul looks annoyed that he missed");
        Game.Instance._changeStateInternal(new GrazgulSecondStageLongRangeAttackState());
    }
}

internal class GrazgulSecondStageLongRangeAttackState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Grazgul shows his dagger again, this time held by its sharp end.");
    }
    public sealed override void Strike()
    {
        Console.WriteLine("Before you strike him with your pickaxe, Grazgul throws the dagger at you. The dagger hits your chest and you fall down to the ground. You feel your life slipping away");
        Game.Instance._changeStateInternal(new FirstBossFailState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You parry the dagger using your pickaxes large surface area. The dagger flies back to Grazgul and hits his left eye. He screams in pain.");
        Game.Instance._changeStateInternal(new GrazgulFinalStageState());
    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You dodge the dagger. Grazgul looks angry");
        Game.Instance._changeStateInternal(new GrazgulSecondStageFootAttackState());
    }

}

internal class GrazgulFinalStageState : State
{
    public override void PrintSituation()
    {
        Console.WriteLine("Grazgul: Raaawwwwrrr my favorite eye!\nGrazgul tries to pick a large rock from the ground.");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You think fast and aim his hearth coming from his left with your pickaxe. He could not see you approach from left.\nYou dig your pickaxe to this heart and try to pull it back.\nHe reactively pushes you away and the pickaxe comes out.\nBlood starts rushing out from Grazgul's large wound just like a river. He is dead.");
        Game.Instance._changeStateInternal(new GrazgulDeathState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You tried parrying a large rock. The rock is too heavy and it first crushes your pickaxe and then crushes your face.");
        Game.Instance._changeStateInternal(new FirstBossFailState());
    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You try to dodge the rock by rolling left. However, this time Grazgul does not throw it right away, and waits for you to finish your roll. \n Then he throws it at you right when you were disoriented. Your lower body is squished. \n The last thing you hear while your vision darkens is 'Drazgul smart' ");
        Game.Instance._changeStateInternal(new FirstBossFailState());
    }
}

internal sealed class GrazgulDeathState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("\n The Angel Appears. \n Angel: I am surprised by your determination. Our decision to make you the chosen one must be right \n Now you have to scale that tower to find Golionerdth and his ring. \n You must destroy it at once. \n Proceed to the tower?");
    }

    public sealed override void Proceed()
    {
        GameContext._changeStateInternal(new ExitMines());
    }
}

internal sealed class ExitMines : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("On your way out of the mines, you stumble upon the armory of the orks. You gear up with some armor, a sword and a shield, then head out on your way towards the tower.");
        GameContext._changeStateInternal(new FirstBridgeApproach());
    }
}

internal sealed class PreDragonShrine : State
{
   public sealed override void PrintSituation()
    {
        Console.WriteLine("You wake up at the angel's shrine, your soul still intact. The dragon awaits...");
    }

    public sealed override void Proceed()
    {
        GameContext._changeStateInternal(new BridgeApproach());
    }
}

internal sealed class FirstBridgeApproach : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("You arrive at a clearing on a hill. In front of you is a black, wide, stone bridge that goes across a steep canyon. The bridge leads to the tower, standing on a solitary peak. \n" +
            "As you walk towards the bridge you start to notice faint markings along the ground. \n" +
            "When you kneel to inspect them you notice they resemble the silhouette of a human, covered in ash. A look around you reveals dozens of these figures, scattered around the path to the bridge. \n" +
            "As you stand up and reach for your sword, you hear a deafening screech from the canyon below and a massive creature soars out of it to the sky. \n" +
            "While it proceeds to land on the bridge, the movement of its wings cause great winds, causing you to almost lose your balance.\n" +
            "After it stops in front of you, you can feel the heat emanating from its belly. A seething flame many adventurers before you had the misfortune to experience first hand.\n" +
            "-----------------------------------------\n" +
            "Great Dragon Rantlok blocks your way.\n" +
            "-----------------------------------------\n" +
            "Proceed?");
    }
    public sealed override void Proceed()
    {
        GameContext._changeStateInternal(new RantlokEncounter(firstEncounter: true));
    }
}

internal sealed class BridgeApproach : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("You are back at the clearing, an obstacle stands between you and your mission.\n"+
            "-----------------------------------------\n" +
            "Great Dragon Rantlok blocks your way.\n" +
            "-----------------------------------------\n" +
            "Proceed?");
    }
    public sealed override void Proceed()
    {
        GameContext._changeStateInternal(new RantlokEncounter(firstEncounter: true));
    }
}

internal sealed class RantlokEncounter : State
{
    private static int _health = 5;
    public RantlokEncounter(bool firstEncounter = false, bool damaged = false)
    {
        if (firstEncounter)
        {
            _health = 5;
        }
        if (damaged)
        {
            _health--;
        }
    }
    private void _displayHealth()
    {
        string healthBar = "";
        for (int i = 0; i < _health; i++)
        {
            healthBar += "-";
        }
        for (int i = 0; i < 5 - _health; i++)
        {
            healthBar += "*";
        }
        Console.WriteLine("Rantlok: " + healthBar);
    }
    public sealed override void PrintSituation()
    {
        _displayHealth();
        if (_health > 1)
        {
            int nextEvent = new Random().Next(1, 4);
            switch (nextEvent)
            {
                case 1:
                    GameContext._changeStateInternal(new RantlokFlameAttack());
                    break;
                case 2:
                    GameContext._changeStateInternal(new RantlokClawAttack());
                    break;
                case 3:
                    GameContext._changeStateInternal(new RantlokTailAttack());
                    break;
                /*
                case 4:
                    GameContext._changeStateInternal(new RantlokFlightAttack());
                    break;
                */
            }
        }
        else
        {
            GameContext._changeStateInternal(new RantlokChargeAttack());
        }
    }
}

internal sealed class RantlokFlameAttack : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Rantlok takes a deep breath while bringing his head back, you notice the red glow around its belly getting brighter.");
    }
    public sealed override void Strike()
    {
        Console.WriteLine("You attempt to slash the dragon, but before you can get close it releases a gust of flame that engulfs your body. Within moments you become another ashen silhouette like the others.");
        GameContext._deathMessageInternal();
        GameContext._changeStateInternal(new PreDragonShrine());
    }
    public sealed override void Parry()
    {
        Console.WriteLine("Scorching fire spews forth from between the dragons jaws. You raise your shield in an attempt to block the incoming flames.\n" +
            "The shield blocks the initial wave but you are quickly surrounded by fire and are severely burnt.");
        GameContext._deathMessageInternal();
        GameContext._changeStateInternal(new PreDragonShrine());
    }
    public sealed override void Dodge()
    {
        Console.WriteLine("You quickly dive to the side, narrowly avoiding the flames spewed by the dragon. You feel the heat on your back as you roll to your feet.\n" +
            "As Rantlok cools down from his attack you make use of the small window of opportunity to slash at his legs.");
        GameContext._changeStateInternal(new RantlokEncounter(damaged: true));
    }
}

internal sealed class RantlokClawAttack : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Rantlok leans slightly to the side and draws his claw back");
    }
    public sealed override void Strike()
    {
        Console.WriteLine("You swing your sword at the dragon in an attempt to stagger him before he can hit you, but he's quite fast for his size and hits you with his claws. They tear into your body and you're flung to the side." +
            "You bleed to death on the ground.");
        GameContext._deathMessageInternal();
        GameContext._changeStateInternal(new PreDragonShrine());
    }
    public sealed override void Parry()
    {
        Console.WriteLine("You raise your shield to block the incoming attack. Rantlok hits you with his claws and and grabs your shield. You use your sword in your other hand to stab his claws.\n" +
            "He screeches in pain and throws you to the side. You quickly get back on your feet and reassume your stance.\n" +
            "You're not going down just yet");
        GameContext._changeStateInternal(new RantlokEncounter(damaged: true));
    }
    public sealed override void Dodge()
    {
        Console.WriteLine("You roll towards Rantlok's incoming claw attack, narrowly avoiding it. He lets out a growl as if he's annoyed by your persistence.");
        GameContext._changeStateInternal(new RantlokEncounter());
    }
}
internal sealed class RantlokTailAttack : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Rantlok turns his side to you and raises his tail behind him.");
    }
    public sealed override void Strike()
    {
        Console.WriteLine("You quickly close the distance towards Rantlok and his tail comes crashing down behind you. You get a quick slash in as he turns his body back towards you.");
        GameContext._changeStateInternal(new RantlokEncounter(damaged: true));
    }
    public sealed override void Parry()
    {
        Console.WriteLine("You crouch on the ground and hold your shield up at an angle. Rantlok's tail hits your shield and bounces off the angled surface.");
        GameContext._changeStateInternal(new RantlokEncounter());
    }
    public sealed override void Dodge()
    {
        Console.WriteLine("You try to roll away but misjudge the length of his tail. It hits you with great speed and strength like a whip.\n" +
            "The impact shatters your ribs which puncture your lungs, throwing you against a large rock. You fall down to the ground and drown in your own blood.");
        GameContext._deathMessageInternal();
        GameContext._changeStateInternal(new PreDragonShrine());
    }
}
/*
internal sealed class RantlokFlightAttack : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Rantlok: Flight Attack");
    }
}
*/
internal sealed class RantlokChargeAttack : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Rantlok moves his feet back, brings himself slightly lower and furls his wings closer to his body. Then, he suddenly springs forward and charges straight toward you.");
    }
    public sealed override void Strike()
    {
        Console.WriteLine("Thinking quickly you drop your shield and start sprinting towards the dragon, sword in hand. Just as he's about to catch you with his jaws you drop to the ground, sliding below his head and between his legs.\n" +
            "You slash at him from below with your sword and hold on to it with all your might, letting the massive dragons momentum carry your blade across his body.\n" +
            "Rantlok howls in pain and falls to the ground. Unable to stop himself or take flight in time, he slides off the edge of the clearing and drops into the canyon below.\n" +
            "-----------------------------------------\n" +
            "Great Dragon Rantlok Slain\n" +
            "-----------------------------------------\n" +
            "Proceed?");
        GameContext._changeStateInternal(new GolionerdhBeginState());
    }
    public sealed override void Parry()
    {
        Console.WriteLine("You try to block Rantlok's charge but he opens his mouth and grabs you in his gaping jaws.\n" +
            "You feel his teeth sinking into your body and are dead by the time he spits you out into the canyon below.");
        GameContext._deathMessageInternal();
        GameContext._changeStateInternal(new PreDragonShrine());
    }
    public sealed override void Dodge()
    {
        Console.WriteLine("You roll out of Rantlok's immediate path. However, already expecting you to dodge, the dragon moves his neck out towards you and catches you with the side of his head.\n" +
            "He quickly flings his neck back ahead and throws you into the canyon below as he takes off. The last thing you see before you crash into the ground is his wings soaring through the sky above you.");
        GameContext._deathMessageInternal();
        GameContext._changeStateInternal(new PreDragonShrine());
    }
}



internal class ThirdBossFailState : State
{
    public sealed override void PrintSituation()
    {
        Game.Instance._deathMessageInternal();
        Console.WriteLine("\n You open your eyes at the shrine again. You are accustomed to dying now. \nYou are determined to get the Dark Ring from Golionerdh. Will you proceed?");
    }
    public sealed override void Proceed()
    {
        Game.Instance._changeStateInternal(new GolionerdhBeginState());
    }
}


internal class GolionerdhBeginState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("You scaled the long tower. At the final floor, you see a menacing figure. That must be the ring bearer \nGolionerdh is looking at you with disdain. You can see the Dark Ring on his index finger.");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You strike at golionerdh. He moves away from your sword in a flash. You couldn't even comprehend his speed.");
        Game.Instance._changeStateInternal(new GolionerdhLightAttackState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You get ready to parry his attack. He does not attack and bursts into a laugh.\nGolionerdh : Do you really want to do this?");
        Game.Instance._changeStateInternal(new GolionerdhLightAttackState());

    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You move to the side in an instant. Golionerdh laughs at you.\nGolionerdh: What a jumpy one you are. That's fun.");
        Game.Instance._changeStateInternal(new GolionerdhLightAttackState());

    }
}

internal class GolionerdhLightAttackState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Golionerdh: So you do really want to fight me. I will reward you for reaching here by a quick death.\n He picks up his sword from its sheath. The sword has runes carved from its hilt to its pointy edge.");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You try to strike at Golionerdth. Before your sword could reach its destination, you feel a warmth in your chest. \nGolionerdh whispers to you : It will all be over soon.");
        Game.Instance._changeStateInternal(new ThirdBossFailState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You get ready to parry his attack. You see a flash moving towards you. You lift your sword. \n Golionerdh's sword hits your sword and pushes you back. You were lucky.");
        Game.Instance._changeStateInternal(new GolionerdhChainAttackState());
    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You move back a bit. However, with his immense speed, Golionerdh covers the distance in an instant and stabs you in the chest. Your life fades away.");
        Game.Instance._changeStateInternal(new ThirdBossFailState());
    }

}

internal class GolionerdhChainAttackState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Golionerdh shakes his left hand and a chain comes out of his armor. He starts rotating the chain above his head.");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You lunge forward with your sword. Golionerdh releases the chain and wraps it around your body. \n He pulls you towards him and shashes your body his sword. You died.");
        Game.Instance._changeStateInternal(new ThirdBossFailState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You try to parry his chain. His chain wraps around your body. He pulls the chain and its sharp edges slash your skin. You fall down in pain.");
        Game.Instance._changeStateInternal(new ThirdBossFailState());
    }

    public sealed override void Dodge()
    {
        Console.WriteLine("Golionerdh releases the chain towards you. You lay down towards the ground. The chain passes over your head and misses you by a few fingers.");
        Game.Instance._changeStateInternal(new GolionerdhBodyDashState());
    }
}

internal class GolionerdhBodyDashState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Golionerdh drops his weapons and dashes towards you with his large body. You are again in awe of his speed.");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You try to strike him with your sword. He just pushes your sword away and jumps on you.");
        Game.Instance._changeStateInternal(new GolionerdhOnTopOfYouState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You try to block him with your sword but he doesnt even care about it and jumps on you.");
        Game.Instance._changeStateInternal(new GolionerdhOnTopOfYouState());

    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You try to dodge his lunge by moving right but he just makes a quick turn towards you and jumps on you.");
        Game.Instance._changeStateInternal(new GolionerdhOnTopOfYouState());
    }
}

internal class GolionerdhOnTopOfYouState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Golionerdh is now sitting on you, restricting your movements and looking at you menacingly. \n Golionerdh: Any last worlds brave one?\nYou see the Dark Ring in his left hand's index finger. \n You have your dagger in your pocket.");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You try to strike his ring finger with your dagger. Your dagger cuts his finger and the ring falls down.");
        Game.Instance._changeStateInternal(new GolionerdhPutRingState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You try to parry his hand. He just pushes your hand away and grabs your neck. \nHe asserts pressure on your neck while you squirm. The world turns dark.");
        Game.Instance._changeStateInternal(new ThirdBossFailState());
    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You try to dodge his hand. He just pushes your hand away and grabs your neck. \nHe asserts pressure on your neck while you squirm. The world turns dark.");
        Game.Instance._changeStateInternal(new ThirdBossFailState());
    }

}

internal class GolionerdhPutRingState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("You grab the ring and put it on your index finger. The ring gets smaller by itself to fit your finger.\nYou feel a surge of energy reverbating in your body.\n Golionerdh looks worried. He grabs his sword again from the ground and rushes towards you.");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You try to lunge forward with your punch. However, you were not expecting to be able to move this fast and pass behing him by a few steps.");
        Game.Instance._changeStateInternal(new GolionerdhRingFinalFight());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You easily parry his sword by an elegant hand motion. You feel like the world around you is moving much slower.");
        Game.Instance._changeStateInternal(new GolionerdhRingFinalFight());
    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You easily dodge his attack with a side step. You feel like you could see his movements before he even makes them.");
        Game.Instance._changeStateInternal(new GolionerdhRingFinalFight());
    }
}

internal class GolionerdhRingFinalFight : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Golionerdh: Raaaaawwwhh give me back my ring. \nHe again lounges towards you with his sword.");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("This time you knew your speed better and aimed your punch right for his heart.\nBefore his sword could hit you, your punch punctured his heart and appeared from the back of his body.");
        Game.Instance._changeStateInternal(new FinalChoiceState());
    }

    public sealed override void Parry()
    {
        Console.WriteLine("You easily parry his sword by an elegant hand motion. You feel like the world around you is moving much slower.");
        Game.Instance._changeStateInternal(new GolionerdhRingFinalFight());
    }

    public sealed override void Dodge()
    {
        Console.WriteLine("You easily dodge his attack with a side step. You feel like you could see his movements before he even makes them.");
        Game.Instance._changeStateInternal(new GolionerdhRingFinalFight());

    }
}

internal class FinalChoiceState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("Golionerdh spurts out blood. \n Golionerdh: I see, you had help from the Angels didn't you. You knew my every move by trial and error.\nGolionerdh: Now you have the Dark Ring. You have the power. Noone can beat you. This world is yours!\n\n Angel: Dont believe his lies, will for the ring to ring to destroy itself and all this suffering your people had will not happen again\nAngel: If you destroy the ring, we will make your wife come back per our gratitude. \n\n Golionerdth: Don't trust the angels. They talk in cryptic ways. Don't forget, the only true thing in this world is power. \n\n Which will you choose? \nKeep the ring to yourself (Strike)\nDestroy the ring (Proceed)");
    }

    public sealed override void Strike()
    {
        Console.WriteLine("You decided to keep the ring. You know by your hearth that you are the strongest person in this world. The feeling of power satisfies you deeply. \nAngel: Very well. We will see you soon. Time is nothing to us. And we will not forget what you did. We never forget. \n\n You conquer the world by yourself and become the god emperor of both humans and orcs. For a thousand years you rein and control the world.\n You forget about your wife and You the angels in those thousand years. However, one day, a person sees a dream in which an entity with many eyes greeted them.");
        Game.Instance._changeStateInternal(new EndState());
    }

    public sealed override void Proceed()
    {
        Console.WriteLine("You decided to destroy the ring. The souls inside of the ring slowly poured out of the broken ring.\nThe angel waited there and picked one of the souls. The soul that the angel touched became more distinguishable and took the shape of your wife, Ezra.\nYou try to hug her but your hands pass through it. You turn towards the angel angrily and ask why you cannot touch her. \n Angel: A soul can only bind into its own body. Your wives body is rotten by now. This is the best we can do.\n The angel leaves.\n\n You go to a far away land with your wives soul following you as a ghastly presence. \nYou live together for 10 years and you get used to her not having a physical body. You still love her. \n One morning you wake up, she seems disturbed. You ask why. \nShe tells you that being with you for the last 10 years were great, but not being able to touch and feel and taste and smell anything made her feel unnatural. \n She asks you whether you would let her go to join the spirit realm as it should be. \n You are sad but you love your her and what she wants is the most important thing for you. \n You let her go and live the rest of your life alone in a cabin in the wilderness.");
        Game.Instance._changeStateInternal(new EndState());
    }
}

internal class EndState : State
{
    public sealed override void PrintSituation()
    {
        Console.WriteLine("\nThe End. Thank you for playing our game.");
    }
}