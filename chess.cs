using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chess : MonoBehaviour {
    char[,] board = new char[3, 3]; //棋盘
    char player1 = 'O'; //玩家1
    char player2 = 'X'; //玩家2
    int count = 0; //表示落下的棋子数
    int flag = 0; //玩家落子顺序,只有1和2
    int state = 0; //0-未结束，1-玩家1胜，2-玩家2胜，3-平局

    int backX = 250, backY = 400, backW = 300, backH = 200;
    int label1X = 400, label1Y = 370, label1W = 100, label1H = 40;
    int labelX = 260, labelY = 450, labelW = 100, labelH = 40;
    int buttonX = 400, buttonY = 400, buttonW = 40, buttonH = 40;
    int resetX = 530, resetY = 420, resetW = 80, resetH = 40;
    int changeX = 530, changeY = 460, changeW = 80, changeH = 40;

    //Game Reset
    void Reset()
    {
        count = 0;
        flag = 1;
        state = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i,j] = ' ';
            }
        }
    }

    //Game Start
    void Start()
    {
        Reset();
    }
    //check the game state
    int check()
    {

        //每行
        for (int i = 0; i < 3; i++)
        {
            if (board[i,0] == board[i,1] && board[i,1] == board[i,2] && board[i,0] != ' ')
            {
                if (board[i,0] == 'O')
                {
                    return 1; //玩家1获胜
                }
                else
                {
                    return 2; //玩家2获胜
                }
            }
        }

        //每列
        for (int i = 0; i < 3; i++)
        {
            if (board[0,i] == board[1,i] && board[1,i] == board[2,i] && board[0, i] != ' ')
            {
                if (board[0,i] == 'O')
                {
                    return 1; //玩家1获胜
                }
                else
                {
                    return 2; //玩家2获胜
                }
            }
        }

        //对角线
        if (board[0,0] == board[1,1] && board[1,1] == board[2,2] && board[0, 0] != ' ')
        {
            if (board[0,0] == 'O')
            {
                return 1; //玩家1获胜
            }
            else
            {
                return 2; //玩家2获胜
            }
        }
        if (board[0,2] == board[1,1] && board[1,1] == board[2,0] && board[2, 0] != ' ')
        {
            if (board[0,2] == 'O')
            {
                return 1; //玩家1获胜
            }
            else
            {
                return 2; //玩家2获胜
            }
        }
        //检查棋盘是否已满
        if (count != 9)
        {
            return 0;
        }
        return 3; //平局
    }

    private void OnGUI()
    {
        GUIStyle temp1 = new GUIStyle
        {
            fontSize = 22
        };
        temp1.normal.textColor = Color.white;
        temp1.fontStyle = FontStyle.Bold;

        GUIStyle temp2 = new GUIStyle
        {
            fontSize = 18,
            fontStyle = FontStyle.BoldAndItalic
        };

        state = check(); //检查游戏结局
        if (state != 0)
        {
            if (state == 1)
            {
                GUI.Label(new Rect(label1X, label1Y, label1W, label1H), "Player 1 Win",temp1);
            }
            else if (state == 2)
            {
                GUI.Label(new Rect(label1X, label1Y, label1W, label1H), "Player 2 Win",temp1);
            }
            else
            {
                GUI.Label(new Rect(label1X, label1Y, label1W, label1H), "No One Win",temp1);
            }
        }
        //重玩游戏
        if (GUI.Button(new Rect(resetX, resetY, resetW, resetH), "Reset"))
        {
            Reset();
        }
        if(GUI.Button(new Rect(changeX, changeY, changeW, changeH), "Exchange") && state != 0)
        {
            flag = 3 - flag;
        }

        //显示是谁的回合
        if (flag == 1)
        {
            GUI.Label(new Rect(labelX, labelY, labelW, labelH), "Player 1's Turn",temp2);
        }
        else
        {
            GUI.Label(new Rect(labelX, labelY, labelW, labelH), "Player 2's Turn",temp2);
        }


        //玩家落子
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {//可落子
                    if (GUI.Button(new Rect(buttonX + i * buttonW, buttonY + j * buttonH, buttonW, buttonH), "" + board[i, j]) && state == 0)
                    {
                        if (flag == 1)
                        {
                            board[i, j] = player1;
                        }
                        else
                        {
                            board[i, j] = player2;
                        }
                        count++; //棋子增加
                        flag = 3 - flag;//交换回合
                    }
                }
                else
                { //不可落子
                    GUI.Button(new Rect(buttonX + i * buttonW, buttonY + j * buttonH, buttonW, buttonH), "" + board[i, j]);
                }
            }
        }

    }


}
