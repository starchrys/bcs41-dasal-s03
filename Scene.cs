using Godot;
using System;

public class Scene : Node2D
{
    private Player1 player;
    private Ball ball;
    private Label score;
    private Label level;
    private Label hS;
    private Label GO_score;
    private Label GO_highscore;
    private Panel gameOver;
    private Panel scoreCont;
    private LineEdit nameInput;
    public int playerScore;
    private bool newHighscore;
    public int playerHighScore = 0;
    public string playerLevel = "Level 1 - Easy";
    private Vector2 ballStart;
    public override void _Ready()
    {
        score = GetNode<Label>("score");
        level = GetNode<Label>("level");
        hS = GetNode<Label>("highScore");
        ball = GetNode<Ball>("Ball");
        gameOver = GetNode<Panel>("GameOver");
        scoreCont = GetNode<Panel>("scoreContainer");
        GO_score = scoreCont.GetNode<Label>("GO_score");
        GO_highscore = scoreCont.GetNode<Label>("GO_highScore");
        player = GetNode<Player1>("Player1");
        nameInput = gameOver.GetNode<LineEdit>("nameInput");

        ballStart = ball.ballStart;
        gameOver.Hide();
        scoreCont.Hide();
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        score.Text = "Score: " + playerScore.ToString();

        if(playerScore > 10){
            playerLevel = "Level 2 - Medium";
            ball.ballSpeed = 400.0f;
        }
        if(playerScore > 20){
            playerLevel = "Level 3 - Hard";
            ball.ballSpeed = 500.0f;
        }
        if(playerScore == 0){
            playerLevel = "level 1 - Easy";
        }

        level.Text = playerLevel;
        getHighScore();
        hS.Text = "High Score: " + playerHighScore;
    }

    private void _on_table_body_entered(object obj)
    {
        if (obj is Ball ball)
        {
            ball.direction.y *= -1;
        }
    }

    private void _on_top_body_entered(object obj)
    {
        if (obj is Ball ball)
        {
            ball.direction.y *= -1;
        }
    }

    private void _on_right_body_entered(object obj)
    {
        if (obj is Ball ball)
        {
            ball.direction.x *= -1;
        }
    }

    private void _on_left_body_entered(object obj)
    {
        if (obj is Ball ball)
        {
            ball.Position = ballStart;
            gameOver.Show();
        }
    }

    private void _on_playerArea_body_entered(object obj)
    {
        if (obj is Ball ball)
        {
            ball.direction.x *= -1;
            playerScore += 1;
        }
    }

    private void _on_MenuButton_pressed()
    {
        gameOver.Hide();
        string name = nameInput.Text;
        CollectData(name, playerScore);
        if(playerScore > playerHighScore){
                hS.Text = "High Score: " + playerScore;
            }
        scoreCont.Show();
        GO_score.Text = "Score: " + playerScore;
        getHighScore();
        GO_highscore.Text = "High Score: " + playerHighScore;
        GD.Print(playerHighScore);
    }

    public void CollectData(String name, int score)
    {
        string data = "Name: " + name + " || Score: " + score;

        File file = new File();
        File clearFile = new File();

        Error openError = file.Open("res://data.txt", File.ModeFlags.ReadWrite);

        if (openError == Error.Ok)
        {
            string existingData = file.GetAsText();
            string[] lines = existingData.Split('\n');
            string firstLine = lines[0].Trim();
            if(firstLine.Length == 12){
                playerHighScore = int.Parse(firstLine.Substring(11, 1));
            } else if (firstLine.Length == 13){
                playerHighScore = int.Parse(firstLine.Substring(11, 2));
            } else if (firstLine.Length == 14){
                playerHighScore = int.Parse(firstLine.Substring(11, 3));
            }
            
            if(playerScore > playerHighScore){
                lines[0] = "Highscore: " + playerScore;
                firstLine = lines[0];
                existingData = string.Join("\n", lines, 1, lines.Length - 1);
                newHighscore = true;
            } else {newHighscore = false;}
            file.Close();

            Error openError2 = clearFile.Open("res://data.txt", File.ModeFlags.Write);

            if (openError2 == Error.Ok)
            {
                file.Close();
            }
            
            Error openError3 = file.Open("res://data.txt", File.ModeFlags.ReadWrite);

            if (openError3 == Error.Ok)
            {
                if(newHighscore == true){
                    file.StoreLine(firstLine);
                }
                file.SeekEnd();
                file.StoreLine(existingData);
                file.StoreLine(data);
                file.Close();
            }
        }
        else
        {
            GD.Print("Failed to open the file: " + openError.ToString());
        }
    }

    public void getHighScore()
    {
        File file = new File();

        Error openError = file.Open("res://data.txt", File.ModeFlags.Read);

        if (openError == Error.Ok)
        {
            string existingData = file.GetAsText();
            string[] lines = existingData.Split('\n');
            string firstLine = lines[0].Trim();
            if(firstLine.Length == 12){
                playerHighScore = int.Parse(firstLine.Substring(11, 1));
            } else if (firstLine.Length == 13){
                playerHighScore = int.Parse(firstLine.Substring(11, 2));
            } else if (firstLine.Length == 14){
                playerHighScore = int.Parse(firstLine.Substring(11, 3));
            }
            file.Close();
        }
        else
        {
            GD.Print("Failed to open the file: " + openError.ToString());
        }
    }

    private void _on_RestartButton_pressed()
    {
        scoreCont.Hide();
        gameOver.Hide();
        player.Position = player.startingPosition;
        ball.Position = ballStart;
        ball.ballSpeed = 300.0f;
        playerScore = 0;
        score.Text = "Score: 0";
        level.Text = "Level 1 - Easy";
        nameInput.Text = "";
    }

    //     public void restartBall()
    // {
    //     Ball ball = (Ball)scene.Instance();

    //     ball.GlobalPosition = ballStart;

    //     AddChild(ball);
    // }
}