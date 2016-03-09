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
   public class Atelier : Microsoft.Xna.Framework.Game
   {
      const float INTERVALLE_CALCUL_FPS = 1f;
      const float INTERVALLE_MAJ_STANDARD = 1f / 60f;
      GraphicsDeviceManager PériphériqueGraphique { get; set; }
      SpriteBatch GestionSprites { get; set; }

      RessourcesManager<SpriteFont> GestionnaireDeFonts { get; set; }
      RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
      RessourcesManager<Model> GestionnaireDeModèles { get; set; }
      RessourcesManager<Effect> GestionnaireDeShaders { get; set; }
      Caméra CaméraJeu { get; set; }

      public InputManager GestionInput { get; private set; }

      public Atelier()
      {
         PériphériqueGraphique = new GraphicsDeviceManager(this);
         Content.RootDirectory = "Content";
         PériphériqueGraphique.SynchronizeWithVerticalRetrace = false;
         IsFixedTimeStep = false;
         IsMouseVisible = true;
      }

      protected override void Initialize()
      {
         const int DIMENSION_TERRAIN = 256;
         Vector2 étenduePlan = new Vector2(DIMENSION_TERRAIN, DIMENSION_TERRAIN);
         Vector2 charpentePlan = new Vector2(4, 3);
         Vector3 positionCaméra = new Vector3(1, 2, 1);
         Vector3 cibleCaméra = new Vector3(0, 0, 0);

         GestionnaireDeFonts = new RessourcesManager<SpriteFont>(this, "Fonts");
         GestionnaireDeTextures = new RessourcesManager<Texture2D>(this, "Textures");
         GestionnaireDeModèles = new RessourcesManager<Model>(this, "Models");
         GestionnaireDeShaders = new RessourcesManager<Effect>(this, "Effects"); 
         GestionInput = new InputManager(this);
         CaméraJeu = new CaméraSubjective(this, positionCaméra, cibleCaméra, Vector3.Up, INTERVALLE_MAJ_STANDARD);

         Components.Add(GestionInput);
         Components.Add(CaméraJeu);
         Components.Add(new Afficheur3D(this));

         #region LIGNE PARKING


         //LignePArking
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-6,1,15), new Vector2(1,98), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-30, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-26, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-22, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-18, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-18, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-30, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-26, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-22, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-52, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-48, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-44, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-40, 1, 25), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-40, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-52, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-48, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-44, 1, 43), new Vector2(1, 20), Color.Gold, INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(20, 1, 25), new Vector2(1, 56), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(35, 1, 25), new Vector2(1, 56), Color.Gold, INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(30, 1, 25), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(30, 1, 29), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(30, 1, 33), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(30, 1, 37), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(30, 1, 41), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(30, 1, 45), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(30, 1, 49), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(30, 1, 53), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));


         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(15, 1, 25), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(15, 1, 29), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(15, 1, 33), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(15, 1, 37), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(15, 1, 41), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(15, 1, 45), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(15, 1, 49), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(15, 1, 53), new Vector2(20, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-52, 1, 30), new Vector2(69, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileColorée(this, 1f, Vector3.Zero, new Vector3(-52, 1, 48), new Vector2(69, 1), Color.Gold, INTERVALLE_MAJ_STANDARD));
         #endregion

         #region PAVILLON ORDINIQUE
         //PAVILLON ORDINIQUE

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50,0,21), new Vector2(20, 10), "MurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(50, 0, 55), new Vector2(20, 10), "MurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(50, 0, 21), new Vector2(68, 10), "MurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(60, 0, 21), new Vector2(68, 10), "MurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturée(this, 1f, Vector3.Zero, new Vector3(50, 5, 21), new Vector2(20, 68), "ToitPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3((49.99f), 0.7f, 25), new Vector2(3,4), "PortePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3((49.99f),3, 24.8f), new Vector2(4,2 ), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3((49.99f), 3, 30.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3((49.99f), 3, 36.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3((49.99f), 3, 42.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3((49.99f), 3, 48.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3((49.99f), 1.5f, 30.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3((49.99f), 1.5f, 36.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3((49.99f), 1.5f, 42.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3((49.99f), 0.7f, 49.1f), new Vector2(3, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));
         
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3((50.2f), 0.7f, 25), new Vector2(3, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3((50.2f), 3, 24.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3((50.2f), 3, 30.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3((50.2f), 3, 36.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3((50.2f), 3, 42.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3((50.2f), 3, 48.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3((50.2f), 1.5f, 30.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3((50.2f), 1.5f, 36.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3((50.2f), 1.5f, 42.8f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3((50.2f), 0.7f, 49.1f), new Vector2(3, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));

         


         //PAVILLON ORDINIQUE INTÉRIEUR -MUR-PLAFOND-PLANCHER

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50, 0, 54.9f), new Vector2(20, 10), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(50, 0, 21.1f), new Vector2(20, 10), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(59.9f, 0, 21), new Vector2(68, 10), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(50.1f, 0, 21), new Vector2(68, 10), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturée(this, 1f, Vector3.Zero, new Vector3(50,0.7f, 21.1f), new Vector2(20, 68), "PlancherIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturée(this, 1f, Vector3.Zero, new Vector3(50, 2.85f, 21.1f), new Vector2(20, 68), "PlancherIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéePlafond(this, 1f, Vector3.Zero, new Vector3(50, 2.71f, 21.1f), new Vector2(20, 68), "PlafondPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéePlafond(this, 1f, Vector3.Zero, new Vector3(50, 4.9f, 21.1f), new Vector2(20, 68), "PlafondPavillon", INTERVALLE_MAJ_STANDARD));
          #endregion


         #region CLASSE PAVILLON ORDINIQUE
         //Mur cLASSE Pavillon Ordinique 
         
         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50, 0, 42f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(50, 0, 41.8f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50, 0, 33.7f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(50, 0, 33.5f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50, 0,48), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(50, 0, 47.99f), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50, 2.71f, 48), new Vector2(9, 4.75f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(50, 2.71f, 47.99f), new Vector2(9, 4.75f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50, 0, 27f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(50, 0, 26.99f), new Vector2(7, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(53.5f, 0, 27f), new Vector2(42f, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(53.49f, 0, 27f), new Vector2(41.8f, 9.9f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(54.5f, 2.71f, 48f), new Vector2(13.7f, 4.75f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(54.49f, 2.71f, 48f), new Vector2(13.7f, 4.75f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

         #endregion


         #region CAFÉ PAVILLON ORDINIQUE
         //Café pavillon Ordinique
         
         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(56.5f, 0, 48), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(56.5f, 0, 47.99f), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(56.5f, 0, 27f), new Vector2(42.1f, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(56.5f, 0, 27f), new Vector2(42.1f, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(56.5f, 0, 27f), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(56.5f, 0, 26.99f), new Vector2(7, 5.1f), "MurIntérieurPavillon", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(56.6f, 0.7f, 40f), new Vector2(2.5f, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(56.4f, 0.7f, 40f), new Vector2(2.5f, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));


         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3((56.55f), 1.3f, 40f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3((56.45f), 1.3f, 40f), new Vector2(4, 2), "FenetrePavillon", INTERVALLE_MAJ_STANDARD));
        
         #endregion 

         #region CASIER PAVILLON
         //CasierPavillon

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(54f, 2.71f, 32f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 32f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 35f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
         //Components.Add(new TuileTexturée(this, 1f, Vector3.Zero, new Vector3(53.5f, 4.21f, 34f), new Vector2(1f, 6f), "beige", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(54f, 0.7f, 32f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 32f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 35f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(54f, 0.7f, 38f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 38f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 41f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(54f, 2.71f, 38f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 38f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 41f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(54f, 0.7f, 44f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 44f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(53.5f, 0.7f, 47f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(54f, 2.71f, 44f), new Vector2(6, 3), "casier", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 44f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical(this, 1f, Vector3.Zero, new Vector3(53.5f, 2.71f, 47f), new Vector2(1f, 3f), "beige", INTERVALLE_MAJ_STANDARD));

         #endregion

         #region PORTE PAVILLON
         //PortePavillon
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(53.51f, 0.7f, 30f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(53.48f, 0.7f, 30f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(53.51f, 2.71f, 30f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(53.48f, 2.71f, 30f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(53.51f, 0.7f, 36f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(53.48f, 0.7f, 36f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(53.51f, 2.71f, 36f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(53.48f, 2.71f, 36f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(53.51f, 0.7f, 42f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(53.48f, 0.7f, 42f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(53.51f, 2.71f, 42f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(53.50f, 2.71f, 42f), new Vector2(2, 3), "PortePavillonInterieur", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(54.51f, 2.71f, 53f), new Vector2(2.5f, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));
         Components.Add(new TuileTexturéeVertical3(this, 1f, Vector3.Zero, new Vector3(54.48f, 2.71f, 53f), new Vector2(2.5f, 4), "PortePavillon", INTERVALLE_MAJ_STANDARD));


        
        
         Components.Add(new TuileTexturéeVertical4(this, 1f, Vector3.Zero, new Vector3(54.515f, 4.3f, 53.5f), new Vector2(0.5f, 0.1f), "P106", INTERVALLE_MAJ_STANDARD));
         #endregion

         #region Tableau Blanc Pavillon 
         //TableauBlanc Pavillon

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50.75f, 1, 41.98f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));
        
         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50.75f, 1, 33.5f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));
         
         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50.75f, 1, 47.8f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50.75f, 3.21f, 41.98f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50.75f, 3.21f, 33.5f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(50.75f, 3.21f, 47.8f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));

         Components.Add(new TuileTexturéeVertical2(this, 1f, Vector3.Zero, new Vector3(51.75f, 3.21f, 54.85f), new Vector2(4, 3), "TableauBlanc", INTERVALLE_MAJ_STANDARD));
//Tableau echelard

         Components.Add(new TuileDiagonalTexturé(this, 1f, Vector3.Zero, new Vector3(50.2f, 3.38f, 53.85f), new Vector2(3f, 3), "ToileProjecteur", INTERVALLE_MAJ_STANDARD));


       // Components.Add(new ObjetDeBase(this,"Computer Table",0.0002f,Vector3.Zero,new Vector3(60f, 4f, 60f)));

         // Components.Add(new ObjetDePatrouille(this, "Computer Table", 1f, new Vector3(0, 0, 0), new Vector3(50.2f, 3.38f, 52.85f), new Vector3(50.2f, 3.38f, 52.85f), 0, 0, INTERVALLE_MAJ_STANDARD));
         #endregion

         #region BAHAYE

         //

          //CIEL
         Components.Add(new Terrain(this, 1f, Vector3.Zero, Vector3.Zero, new Vector3(DIMENSION_TERRAIN , 3, DIMENSION_TERRAIN), "LionelEssai4", "TextureEssai2", 3, INTERVALLE_MAJ_STANDARD));
         ////Components.Add(new Terrain(this, 1f, Vector3.Zero, Vector3.Zero, new Vector3(DIMENSION_TERRAIN/5, 10, DIMENSION_TERRAIN/5), "LionelEssai3", "Texture1Essai", 2, INTERVALLE_MAJ_STANDARD));
         Components.Add(new PlanTexturé(this, 1f, new Vector3(0, MathHelper.PiOver2, 0), new Vector3(-DIMENSION_TERRAIN / 2, DIMENSION_TERRAIN / 2, 0), étenduePlan, charpentePlan, "CielGauche", INTERVALLE_MAJ_STANDARD));
         Components.Add(new PlanTexturé(this, 1f, new Vector3(0, -MathHelper.PiOver2, 0), new Vector3(DIMENSION_TERRAIN / 2, DIMENSION_TERRAIN / 2, 0), étenduePlan, charpentePlan, "CielDroite", INTERVALLE_MAJ_STANDARD));
         Components.Add(new PlanTexturé(this, 1f, Vector3.Zero, new Vector3(0, DIMENSION_TERRAIN / 2, -DIMENSION_TERRAIN / 2), étenduePlan, charpentePlan, "CielAvant", INTERVALLE_MAJ_STANDARD));
         Components.Add(new PlanTexturé(this, 1f, new Vector3(0, -MathHelper.Pi, 0), new Vector3(0, DIMENSION_TERRAIN / 2, DIMENSION_TERRAIN / 2), étenduePlan, charpentePlan, "CielArrière", INTERVALLE_MAJ_STANDARD));
         Components.Add(new PlanTexturé(this, 1f, new Vector3(MathHelper.PiOver2, 0, 0), new Vector3(0, DIMENSION_TERRAIN - 1, 0), étenduePlan, charpentePlan, "CielDessus", INTERVALLE_MAJ_STANDARD));

         //Components.Add(new ObjetDePatrouille(this, "Computer Table", 0.002f, new Vector3(0, MathHelper.Pi, -MathHelper.PiOver2), positionARC170, positionCylindre1, 36, 6f, INTERVALLE_MAJ_STANDARD));
         //Components.Add(new ObjetDePatrouille(this, "Feisar", 0.01f, Vector3.Zero, positionFeisar, positionCylindre2, 24, 8,));
//Components.Add(new ObjetDePatrouille(this, "Airplane_blue", 1f, new Vector3(0, 0, MathHelper.PiOver4), positionBiplan, positionCylindre3, 36, 7, INTERVALLE_MAJ_STANDARD));

         //Components.Add(new Cylindre(this, 1f, Vector3.Zero, positionCylindre1, new Vector2(10f, 20f), new Vector2(30, 30), "SQWAD", INTERVALLE_MAJ_STANDARD));
         //Components.Add(new Cylindre(this, 1f, new Vector3(0, 0, 0), positionCylindre2, new Vector2(10f, 20f), new Vector2(30, 30), "SQWAD", INTERVALLE_MAJ_STANDARD));
         //Components.Add(new Cylindre(this, 1f, Vector3.Zero, positionCylindre3, new Vector2(10f, 20f), new Vector2(30, 30), "SQWAD", INTERVALLE_MAJ_STANDARD));
         Components.Add(new AfficheurFPS(this, "Arial20", Color.Gold, INTERVALLE_CALCUL_FPS));

         Services.AddService(typeof(Random), new Random());
         Services.AddService(typeof(RessourcesManager<SpriteFont>), GestionnaireDeFonts);
         Services.AddService(typeof(RessourcesManager<Texture2D>), GestionnaireDeTextures);
         Services.AddService(typeof(RessourcesManager<Model>), GestionnaireDeModèles);
         Services.AddService(typeof(RessourcesManager<Effect>), GestionnaireDeShaders);
         Services.AddService(typeof(InputManager), GestionInput);
         Services.AddService(typeof(Caméra), CaméraJeu);
         GestionSprites = new SpriteBatch(GraphicsDevice);
         Services.AddService(typeof(SpriteBatch), GestionSprites);
         base.Initialize();
      }

      protected override void Update(GameTime gameTime)
      {
         GérerClavier();
         base.Update(gameTime);
      }

      private void GérerClavier()
      {
         Vector3 cibleCaméra = new Vector3(0, 0, 0);
         Vector3 positionCaméra0 = new Vector3(0, 250, 125);
         Vector3 positionCaméra1 = new Vector3(0, 300, 0F);

         //Vector3 positionCaméra2 = new Vector3(-20, 30, 30);
         Vector3 positionCaméra2 = new Vector3(30, 30, -20);
         Vector3 cibleCaméra2 = new Vector3(-20, 10, -20);

         Vector3 positionCaméra3 = new Vector3(-90, 30, -140);
         Vector3 cibleCaméra3 = new Vector3(-90, 10, -90);

         Vector3 positionCaméra4 = new Vector3(-75, 20, 50);
         Vector3 cibleCaméra4 = new Vector3(-90, 10, 90);

         if (GestionInput.EstEnfoncée(Keys.Escape))
         {
            Exit();
         }
         if (!(GestionInput.EstEnfoncée(Keys.LeftShift) || GestionInput.EstEnfoncée(Keys.RightShift) ||
               GestionInput.EstEnfoncée(Keys.LeftControl) || GestionInput.EstEnfoncée(Keys.RightControl)))
         {
            if (GestionInput.EstNouvelleTouche(Keys.D1) || GestionInput.EstNouvelleTouche(Keys.NumPad1))
            {
               CaméraJeu.Déplacer(positionCaméra0, cibleCaméra, Vector3.Up);
            }
            else
            {
               if (GestionInput.EstNouvelleTouche(Keys.D2) || GestionInput.EstNouvelleTouche(Keys.NumPad2))
               {
                  CaméraJeu.Déplacer(positionCaméra1, cibleCaméra, Vector3.Forward);
               }
               else
               {
                  if (GestionInput.EstNouvelleTouche(Keys.D3) || GestionInput.EstNouvelleTouche(Keys.NumPad3))
                  {
                     CaméraJeu.Déplacer(positionCaméra2, cibleCaméra2, Vector3.Up);
                  }
                  else
                  {
                     if (GestionInput.EstNouvelleTouche(Keys.D4) || GestionInput.EstNouvelleTouche(Keys.NumPad5))
                     {
                        CaméraJeu.Déplacer(positionCaméra3, cibleCaméra3, Vector3.Up);
                     }
                     else
                     {
                        if (GestionInput.EstNouvelleTouche(Keys.D5) || GestionInput.EstNouvelleTouche(Keys.NumPad5))
                        {
                           CaméraJeu.Déplacer(positionCaméra4, cibleCaméra4, Vector3.Up);
                        }
                     }
                  }
               }
            }
         }
      }

      protected override void Draw(GameTime gameTime)
      {
         GraphicsDevice.Clear(Color.White);
         base.Draw(gameTime);
      }
   }
}


#endregion
