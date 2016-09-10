using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{

    enum EPlayerMode
    {
        None,
        View,
        Build
    }




    class Player
    {
        //Wie bei den Gamestates, wenn sich der Modus ändert, dann wird was geändert.
        EPlayerMode mode;
        EPlayerMode prevMode;

        EBuilding[] EBuildings = {EBuilding.MainBuilding,
        EBuilding.Mill,
        EBuilding.FishingHut,
        EBuilding.Stonemason,
        EBuilding.Woodcutter};

        int currBuild = 0;



        //Das ausgewählte Gebäude zum bauen
        Building targetBuild;
        EBuilding targetTypeBuild;

        ContentManager Content;

        public Player(ContentManager cont)
        {
            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);

            mode = EPlayerMode.View;
            prevMode = EPlayerMode.None;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        void HandlePlayerMode()
        {

            if (InputHandler.Instance.IsKeyPressedOnce(Keys.B))
            {
                mode = EPlayerMode.Build;
            }

            if (InputHandler.Instance.IsKeyPressedOnce(Keys.V))
            {
                mode = EPlayerMode.View;
            }


            //Falls sich der Modus ändert, wird der Modus an andere Handler weitergegeben
            if (prevMode != mode)
            {
                GUIHandler.Instance.gui.plyMode = mode;

                switch (mode)
                {
                    case EPlayerMode.View:
                        GUIHandler.Instance.gui.SetMarkSize(new Point(BuildingHandler.Instance.map.tilesize, BuildingHandler.Instance.map.tilesize));
                        break;
                    case EPlayerMode.Build:
                        targetTypeBuild = EBuildings[currBuild];
                        break;
                    default:
                        break;
                }



            }


            if (InputHandler.Instance.IsKeyPressedOnce(Keys.D1))
                currBuild++;
            if (InputHandler.Instance.IsKeyPressedOnce(Keys.D2))
                currBuild--;

            currBuild = MathHelper.Clamp(currBuild, 0, (int)EBuilding.Count - 1);



            switch (mode)
            {
                case EPlayerMode.Build:
                    targetTypeBuild = EBuildings[currBuild];
                    switch (targetTypeBuild)
                    {


                        case EBuilding.MainBuilding:
                            targetBuild = new MainBuilding(GUIHandler.Instance.gui.markBounds.Location.ToVector2(), Content);
                            break;
                        case EBuilding.FishingHut:
                            targetBuild = new BFishingHut(GUIHandler.Instance.gui.markBounds.Location.ToVector2(), Content);
                            break;
                        case EBuilding.Mill:
                            targetBuild = new BMill(GUIHandler.Instance.gui.markBounds.Location.ToVector2(), Content);
                            break;
                        case EBuilding.Stonemason:
                            targetBuild = new BStonemason(GUIHandler.Instance.gui.markBounds.Location.ToVector2(), Content);
                            break;
                        case EBuilding.Woodcutter:
                            targetBuild = new BWoodCutter(GUIHandler.Instance.gui.markBounds.Location.ToVector2(), Content);
                            break;
                    }
                    if (targetBuild != null)
                    {
                        GUIHandler.Instance.gui.SetBuilding(targetBuild);
                        GUIHandler.Instance.gui.SetMarkSize(targetBuild.Bounds.Size);


                        if (InputHandler.Instance.IsLeftMouseButtonPressedOnce() && BuildingHandler.Instance.map.Buildable(GUIHandler.Instance.gui.markBounds))
                            BuildingHandler.Instance.map.Build(GUIHandler.Instance.gui.markBounds, targetBuild);
                    }

                    break;
                default:
                    break;
            }
            prevMode = mode;
        }

        public void Update(GameTime gTime)
        {

            Vector2 position = CameraHandler.Instance.screenCamera.position;

            Viewport viewport = CameraHandler.Instance.screenCamera.viewport;

            if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && position.Y > 0)
                position.Y -= CameraHandler.Instance.screenCamera.speed * gTime.ElapsedGameTime.Milliseconds;

            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && position.Y + viewport.Height < BuildingHandler.Instance.map.bounds.Height)
                position.Y += CameraHandler.Instance.screenCamera.speed * gTime.ElapsedGameTime.Milliseconds;
            if ((Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) && position.X > 0)
                position.X -= CameraHandler.Instance.screenCamera.speed * gTime.ElapsedGameTime.Milliseconds;
            if ((Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) && position.X + viewport.Width < BuildingHandler.Instance.map.bounds.Width)
                position.X += CameraHandler.Instance.screenCamera.speed * gTime.ElapsedGameTime.Milliseconds;

            CameraHandler.Instance.screenCamera.position = position;

            HandlePlayerMode();

        }
    }


}
