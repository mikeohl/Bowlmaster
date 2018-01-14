using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    private ActionMaster actionMaster;

    [SetUp]
    public void Setup ()
    {
        actionMaster = new ActionMaster();
    }
    

    [Test]
    public void T00_PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01_FirstStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02_Bowl8ReturnsTidy ()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03_2ndBowlReturnsEndTurn ()
    {
        actionMaster.Bowl(1);
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }

    [Test]
    public void T04_Strike1InLastFrame ()
    {
        for (int i = 0; i < 9; i++)
        {
            actionMaster.Bowl(10);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T05_Strike2InLastFrame()
    {
        for (int i = 0; i < 10; i++)
        {
            actionMaster.Bowl(10);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T06_Strike3InLastFrame()
    {
        for (int i = 0; i < 11; i++)
        {
            actionMaster.Bowl(10);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T07_Open1InLastFrame()
    {
        for (int i = 0; i < 9; i++)
        {
            actionMaster.Bowl(10);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(1));
    }

    [Test]
    public void T08_Spare1and2InLastFrame()
    {
        for (int i = 0; i < 9; i++)
        {
            actionMaster.Bowl(10);
        }
        actionMaster.Bowl(1);
        Assert.AreEqual(reset, actionMaster.Bowl(9));
    }

    [Test]
    public void T09_Open1and2InLastFrame()
    {
        for (int i = 0; i < 9; i++)
        {
            actionMaster.Bowl(10);
        }
        actionMaster.Bowl(1);
        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }

    [Test]
    public void T10_Strike1Open2InLastFrame()
    {
        for (int i = 0; i < 9; i++)
        {
            actionMaster.Bowl(10);
        }
        actionMaster.Bowl(10);
        Assert.AreEqual(tidy, actionMaster.Bowl(9));
    }

    [Test]
    public void T11_Strike1Open2Spare3InLastFrame()
    {
        for (int i = 0; i < 9; i++)
        {
            actionMaster.Bowl(10);
        }
        actionMaster.Bowl(10);
        actionMaster.Bowl(1);
        Assert.AreEqual(endGame, actionMaster.Bowl(9));
    }

    [Test]
    public void T12_0and10Spareand1to9NextFrameReturnsTidy()
    {
        actionMaster.Bowl(0);
        actionMaster.Bowl(10);
        Assert.AreEqual(tidy, actionMaster.Bowl(9));
    }

    [Test]
    public void T13_LastFrameAllStrikesReturnsEndGame()
    {
        for (int i = 0; i < 9; i++)
        {
            actionMaster.Bowl(10);
        }
        actionMaster.Bowl(10);
        actionMaster.Bowl(10);
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }
}
