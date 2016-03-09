using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace AtelierXNA
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Jeu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        const float INTERVALLE_MAJ_STANDARD = 1f / 60f;
        const int DIMENSION_TERRAIN = 256;
        public List<BoundingBox> ListeBB { get; private set; }
        public Jeu(Game game)
            : base(game)
        {}

        public override void Initialize()
        {
            Vector2 étenduePlan = new Vector2(DIMENSION_TERRAIN, DIMENSION_TERRAIN);
            Vector2 charpentePlan = new Vector2(4, 3);
                     #region LIGNE PARKING


                     //LignePArking
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-6,1,15), new Vector2(1,98), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-30, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-26, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-22, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-18, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-18, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-30, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-26, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-22, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-52, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-48, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-44, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-40, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-40, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-52, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-48, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-44, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(20, 1, 25), new Vector2(1, 56), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(35, 1, 25), new Vector2(1, 56), Color.Gold, INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(30, 1, 25), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(30, 1, 29), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(30, 1, 33), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(30, 1, 37), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(30, 1, 41), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(30, 1, 45), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(30, 1, 49), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(30, 1, 53), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));


                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(15, 1, 25), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(15, 1, 29), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(15, 1, 33), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(15, 1, 37), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(15, 1, 41), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(15, 1, 45), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(15, 1, 49), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(15, 1, 53), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-52, 1, 30), new Vector2(69, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileColorée(Game, 1f, Vector3.Zero, new Vector3(-52, 1, 48), new Vector2(69, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
                     #endregion

                     #region PAVILLON ORDINIQUE
                     //PAVILLON ORDINIQUE

                     Game.Components.Add(new TuileTexturéeVertical(Game,2, 1f, Vector3.Zero, new Vector3(50, 0, 21), new Vector2(20, 10), "MurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game,1, 1f, Vector3.Zero, new Vector3(50, 0, 55), new Vector2(20, 10), "MurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3(50, 0, 21), new Vector2(68, 10), "MurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(60, 0, 21), new Vector2(68, 10), "MurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuilePlancherTexturé(Game, 1f, Vector3.Zero, new Vector3(50, 5, 21), new Vector2(20, 68), "ToitPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game,3, 1f, Vector3.Zero, new Vector3((49.99f), 0.7f, 25), new Vector2(3, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3((49.99f), 3, 24.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3((49.99f), 3, 30.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3((49.99f), 3, 36.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3((49.99f), 3, 42.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3((49.99f), 3, 48.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3((49.99f), 1.5f, 30.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3((49.99f), 1.5f, 36.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3((49.99f), 1.5f, 42.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3((49.99f), 0.7f, 49.1f), new Vector2(3, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3((50.2f), 0.7f, 25), new Vector2(3, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3((50.2f), 3, 24.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3((50.2f), 3, 30.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3((50.2f), 3, 36.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3((50.2f), 3, 42.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3((50.2f), 3, 48.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3((50.2f), 1.5f, 30.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3((50.2f), 1.5f, 36.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3((50.2f), 1.5f, 42.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3((50.2f), 0.7f, 49.1f), new Vector2(3, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));




                     //PAVILLON ORDINIQUE INTÉRIEUR -MUR-PLAFOND-PLANCHER

                     Game.Components.Add(new TuileTexturéeVertical(Game,2, 1f, Vector3.Zero, new Vector3(50, 0, 54.9f), new Vector2(20, 10), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game,1, 1f, Vector3.Zero, new Vector3(50, 0, 21.1f), new Vector2(20, 10), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game,3, 1f, Vector3.Zero, new Vector3(59.9f, 0, 21), new Vector2(68, 10), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(50.1f, 0, 21), new Vector2(68, 10), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuilePlancherTexturé(Game, 1f, Vector3.Zero, new Vector3(50, 0.7f, 21.1f), new Vector2(20, 68), "PlancherIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuilePlancherTexturé(Game, 1f, Vector3.Zero, new Vector3(50, 2.85f, 21.1f), new Vector2(20, 68), "PlancherIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéePlafond(Game, 1f, Vector3.Zero, new Vector3(50, 2.71f, 21.1f), new Vector2(20, 68), "PlafondPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéePlafond(Game, 1f, Vector3.Zero, new Vector3(50, 4.9f, 21.1f), new Vector2(20, 68), "PlafondPavillon", INTERVALLE_MAJ_STANDARD));
                      #endregion

                      //Mur cLASSE Pavillon Ordinique 

                     Game.Components.Add(new TuileTexturéeVertical(Game,2, 1f, Vector3.Zero, new Vector3(50, 0, 42f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(50, 0, 41.8f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(50, 0, 33.7f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(50, 0, 33.5f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(50, 0, 48), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(50, 0, 47.99f), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(50, 2.71f, 48), new Vector2(9, 4.75f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(50, 2.71f, 47.99f), new Vector2(9, 4.75f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(50, 0, 27f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(50, 0, 26.99f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(53.5f, 0, 27f), new Vector2(42f, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3(53.49f, 0, 27f), new Vector2(41.8f, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(54.5f, 2.71f, 48f), new Vector2(13.7f, 4.75f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3(54.49f, 2.71f, 48f), new Vector2(13.7f, 4.75f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

                      //Café pavillon Ordinique

                     Game.Components.Add(new TuileTexturéeVertical(Game,2, 1f, Vector3.Zero, new Vector3(56.5f, 0, 48), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(56.5f, 0, 47.99f), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(56.5f, 0, 27f), new Vector2(42.1f, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3(56.5f, 0, 27f), new Vector2(42.1f, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(56.5f, 0, 27f), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(56.5f, 0, 26.99f), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

                      //CasierPavillon

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(54f, 2.71f, 32f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 32f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 35f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
                     //Components.Add(new TuileTexturée(this, 1f, Vector3.Zero, new Vector3(53.5f, 4.21f, 34f), new Vector2(1f, 6f), "beige", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(54f, 0.7f, 32f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 32f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 35f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(54f, 0.7f, 38f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 38f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 41f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(54f, 2.71f, 38f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 38f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 41f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(54f, 0.7f, 44f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 44f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 47f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(54f, 2.71f, 44f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 44f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 1,1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 47f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));



                      //PortePavillon
                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(53.51f, 0.7f, 30f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3(53.48f, 0.7f, 30f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(53.51f, 2.71f, 30f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3(53.48f, 2.71f, 30f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(53.51f, 0.7f, 36f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3(53.48f, 0.7f, 36f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(53.51f, 2.71f, 36f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3(53.48f, 2.71f, 36f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(53.51f, 0.7f, 42f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3(53.48f, 0.7f, 42f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(53.51f, 2.71f, 42f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3(53.50f, 2.71f, 42f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(54.51f, 2.71f, 53f), new Vector2(2.5f, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new TuileTexturéeVertical(Game, 3,1f, Vector3.Zero, new Vector3(54.48f, 2.71f, 53f), new Vector2(2.5f, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));


                     Game.Components.Add(new TuileTexturéeVertical(Game, 4,1f, Vector3.Zero, new Vector3(54.515f, 4.3f, 53.5f), new Vector2(0.5f, 0.1f), "P106", INTERVALLE_MAJ_STANDARD));

                      //TableauBlanc Pavillon

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(50.75f, 1, 41.98f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(50.75f, 1, 33.5f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(50.75f, 1, 47.8f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(50.75f, 3.21f, 41.98f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(50.75f, 3.21f, 33.5f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(50.75f, 3.21f, 47.8f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));

                     Game.Components.Add(new TuileTexturéeVertical(Game, 2,1f, Vector3.Zero, new Vector3(51.75f, 3.21f, 54.85f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));
            //Tableau echelard

                     Game.Components.Add(new TuileDiagonalTexturé(Game, 1f, Vector3.Zero, new Vector3(50.2f, 3.38f, 53.85f), new Vector2(3f, 3), "ToileProjecteur", INTERVALLE_MAJ_STANDARD));

                     //Game.Components.Add(new ObjetDeBase(Game, "Computer Table", 0.0002f, Vector3.Zero, new Vector3(60f, 4f, 60f)));

                    // Components.Add(new ObjetDePatrouille(this, "Computer Table", 1f, new Vector3(0, 0, 0), new Vector3(50.2f, 3.38f, 52.85f), new Vector3(50.2f, 3.38f, 52.85f), 0, 0, INTERVALLE_MAJ_STANDARD));


                      //CIEL
                     Game.Components.Add(new Terrain(Game, 1f, Vector3.Zero, Vector3.Zero, new Vector3(DIMENSION_TERRAIN, 3, DIMENSION_TERRAIN), "LionelEssai4", "TextureEssai2", 3, INTERVALLE_MAJ_STANDARD));
                     ////Components.Add(new Terrain(this, 1f, Vector3.Zero, Vector3.Zero, new Vector3(DIMENSION_TERRAIN/5, 10, DIMENSION_TERRAIN/5), "LionelEssai3", "Texture1Essai", 2, INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new PlanTexturé(Game, 1f, new Vector3(0, MathHelper.PiOver2, 0), new Vector3(-DIMENSION_TERRAIN / 2, DIMENSION_TERRAIN / 2, 0), étenduePlan, charpentePlan, "CielGauche", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new PlanTexturé(Game, 1f, new Vector3(0, -MathHelper.PiOver2, 0), new Vector3(DIMENSION_TERRAIN / 2, DIMENSION_TERRAIN / 2, 0), étenduePlan, charpentePlan, "CielDroite", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new PlanTexturé(Game, 1f, Vector3.Zero, new Vector3(0, DIMENSION_TERRAIN / 2, -DIMENSION_TERRAIN / 2), étenduePlan, charpentePlan, "CielAvant", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new PlanTexturé(Game, 1f, new Vector3(0, -MathHelper.Pi, 0), new Vector3(0, DIMENSION_TERRAIN / 2, DIMENSION_TERRAIN / 2), étenduePlan, charpentePlan, "CielArrière", INTERVALLE_MAJ_STANDARD));
                     Game.Components.Add(new PlanTexturé(Game, 1f, new Vector3(MathHelper.PiOver2, 0, 0), new Vector3(0, DIMENSION_TERRAIN - 1, 0), étenduePlan, charpentePlan, "CielDessus", INTERVALLE_MAJ_STANDARD));

            base.Initialize();
        }
        protected override void LoadContent()
        {

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
