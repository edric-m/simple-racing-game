import model.*;

import java.awt.*;
import javax.swing.*;

import java.awt.event.*;
import javax.swing.*;
import javax.swing.event.*;

public class Root extends JFrame
{   
	public static void main(String[] args)
    {   
		new Root();    
	}

	private int x = 500;    private int y = 500;
	
    public Root()
    {   
		setup();
        build();
        pack();
        setVisible(true);   
	}
        
    private void setup()
    {  
		setLocation(x, y);
		setDefaultCloseOperation(EXIT_ON_CLOSE); 
	}
        
	//build the panel here. fill the contents with window objects
    private void build()
    {   
		 add(new Game());
	}
}

public class Game extends JFrame 
{
	String[] folder;
	Track track;
	Car car;
	bool paused=false,started=false;
	int score = 0, best;
	
	private java.awt.Image image;
    private java.awt.Graphics2D graphics2D;
	
	public Game()
	{
		folder = "C:\\Documents and Settings\\Edric\\Desktop\\mon c#\\RacingGameAssignment\\RacingGame\\gameResources\\";
		track = new Track(folder + "Track.txt");
		car = new Car(5);
		setDoubleBuffered(true);
		
		//get the best score
            using (StreamReader reader = new StreamReader(folder + "BestScore.txt"))
            {
                best = Convert.ToInt32(reader.ReadLine());
                reader.Close();
            }
			
		setup();
		build();
	}
	
	@Override
    public void paintComponent(java.awt.Graphics g)
    {
        if(image == null)
        {
            image = createImage(getSize().width, getSize().height);
            graphics2D = (java.awt.Graphics2D)image.getGraphics();
            graphics2D.setRenderingHint(java.awt.RenderingHints.KEY_ANTIALIASING, 
                    java.awt.RenderingHints.VALUE_ANTIALIAS_ON);
            clear();
        }
        
        g.drawImage(image, 0, 0, null);
		
		ValidateMove();

		base.OnPaint(e);
		Graphics g = e.Graphics;

		//draw track based on car position,
		//the car in the correct position,
		//and the score
		int ctr = 500, remainder, trackRow;
		int x, y;
		for (y = 0; y < 12; y++)
		{
			trackRow = ((ctr + player1.PosY) / 50) - 2;
			remainder = (ctr + player1.PosY) % 50;
			for (x = 0; x < 9; x++)
			{
				switch (track.array[trackRow, x])
				{
					case 'T':
						g.DrawImage(track.road, new Point((x * 50), ((y * 50) + remainder - 50)));
						break;
					case 'G':
						g.DrawImage(track.grass, new Point((x * 50), ((y * 50) + remainder - 50)));
						break;
					case 'R':
						g.DrawImage(track.rock, new Point((x * 50), ((y * 50) + remainder - 50)));
						break;
					case 'E':
						g.DrawImage(track.end, new Point((x * 50), ((y * 50) + remainder - 50)));
						break;
				}
			}
			ctr = ctr - 50;
		}
		g.DrawImage(player1.image, new Point((player1.posX), (400)));
		g.DrawString("Score: " + Convert.ToString(score)+"\t\t"+"Best score: "+Convert.ToString(best), new Font("Arial", 16), new SolidBrush(Color.White), 0, 0);
		score++;

		CheckForPause();
    }
	private void setup()
	{
		
	}
	
	private void build()
	{
		
	}
}

public class Car 
{
	public int posX;
	public int PosY;
	public int speed;
	public bool up,down,left,right;
	public int topSpeed;
	private string folder;
	public Bitmap image;

	public Car(int a_topSpeed)
	{
		folder = "C:\\Documents and Settings\\Edric\\My Documents\\mon c#\\RacingGameAssignment\\RacingGame\\gameResources\\";
		image = new Bitmap(folder + "car.bmp");
		posX = 200;
		PosY = 150;
		speed = 0;
		up = false;
		down = false;
		left = false;
		right = false;
		topSpeed = a_topSpeed;
	}

	public void Accelerate(char frontTile)
	{
		if (up)
		{
			switch (frontTile)
			{
				case 'T':
					speed = topSpeed;
					break;
				case 'R':
					speed = 0;
					break;
				case 'G':
					speed = topSpeed / 2;
					break;
			}
			PosY = PosY + speed;
		}
	}

	public void Reverse(char backTile)
	{
		if (down)
		{
			if (backTile != 'R')
			{
				PosY = PosY - 1;
			}
		}
	}

	public void MoveLeft(char lf, char lb)
	{
		if (left)
		{
			switch (speed)
			{
				case 0:
					if (lb != 'R')
					{
						posX = posX - 5;
					}
					break;
				default:
					if (lf != 'R' && lb != 'R')
					{
						posX = posX - 5;
					}
					break;
			}
		}
	}

	public void MoveRight(char rf, char rb)
	{
		if (right)
		{
			switch (speed)
			{
				case 0:
					if (rb != 'R')
					{
						posX = posX + 5;
					}
					break;
				default:
					if (rf != 'R' && rb != 'R')
					{
						posX = posX + 5;
					}
					break;
			}
		}
	}
}

public class Track
{
	private String[] folder;
	public char[,] array = new char[100, 9];
	public Bitmap road;
	public Bitmap grass;
	public Bitmap rock;
	public Bitmap end;

	public Track(string a_trackPath)
	{
		folder = "C:\\Documents and Settings\\Edric\\Desktop\\mon c#\\RacingGameAssignment\\RacingGame\\gameResources\\";
		road = new Bitmap(folder + "road.bmp");
		grass = new Bitmap(folder + "grass.bmp");
		rock = new Bitmap(folder + "rock.bmp");
		end = new Bitmap(folder + "end.bmp");

		string line;
		int x = 0;
		using (StreamReader reader = new StreamReader(a_trackPath))
		{
			while (!reader.EndOfStream)
			{
				line = reader.ReadLine();
				array[x, 0] = line[0];
				array[x, 1] = line[1];
				array[x, 2] = line[2];
				array[x, 3] = line[3];
				array[x, 4] = line[4];
				array[x, 5] = line[5];
				array[x, 6] = line[6];
				array[x, 7] = line[7];
				array[x, 8] = line[8];
				x++;
			}
			reader.Close();
		}
	}
}
