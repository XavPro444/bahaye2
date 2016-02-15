using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AtelierXNA
{
   public class CaméraSubjective : Caméra
   {
      const float INTERVALLE_MAJ_STANDARD = 1f / 60f;
      const float ACCÉLÉRATION = 0.001f;
      const int DIMENSION_TERRAIN = 250;
      const float VITESSE_INITIALE_ROTATION = 1;
      const float VITESSE_INITIALE_TRANSLATION = 1
          ;
      const float DELTA_LACET = MathHelper.Pi / 180; // 1 degré à la fois
      const float DELTA_TANGAGE = MathHelper.Pi / 180; // 1 degré à la fois
      const float DELTA_ROULIS = MathHelper.Pi / 180; // 1 degré à la fois
      const float RAYON_COLLISION = 1f;

      Vector3 Direction { get; set; }
      Vector3 Latéral { get; set; }
      Vector3 Orientation { get; set; }
      float VitesseTranslation { get; set; }
      float VitesseRotation { get; set; }

      float IntervalleMAJ { get; set; }
      float TempsÉcouléDepuisMAJ { get; set; }
      InputManager GestionInput { get; set; }

      bool estEnZoom;
      bool EstEnZoom
      {
         get { return estEnZoom; }
         set
         {
            float ratioAffichage = Game.GraphicsDevice.Viewport.AspectRatio;
            estEnZoom = value;
            if (estEnZoom)
            {
               CréerVolumeDeVisualisation(OUVERTURE_OBJECTIF / 2, ratioAffichage, DISTANCE_PLAN_RAPPROCHÉ, DISTANCE_PLAN_ÉLOIGNÉ);
            }
            else
            {
               CréerVolumeDeVisualisation(OUVERTURE_OBJECTIF, ratioAffichage, DISTANCE_PLAN_RAPPROCHÉ, DISTANCE_PLAN_ÉLOIGNÉ);
            }
         }
      }

      public CaméraSubjective(Game jeu, Vector3 positionCaméra, Vector3 cible, Vector3 orientation, float intervalleMAJ)
         : base(jeu)
      {
         IntervalleMAJ = intervalleMAJ;
         CréerVolumeDeVisualisation(OUVERTURE_OBJECTIF, DISTANCE_PLAN_RAPPROCHÉ, DISTANCE_PLAN_ÉLOIGNÉ);
         CréerPointDeVue(positionCaméra, cible, orientation);
         Cible = cible;
         Orientation = orientation;
         EstEnZoom = false;
      }

      public override void Initialize()
      {
         VitesseRotation = VITESSE_INITIALE_ROTATION;
         VitesseTranslation = VITESSE_INITIALE_TRANSLATION;
         TempsÉcouléDepuisMAJ = 0;
         base.Initialize();
         GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;
      }

      protected override void CréerPointDeVue()
      {
         Direction = Vector3.Normalize(Direction);
         OrientationVerticale = Vector3.Normalize(OrientationVerticale);
         Latéral = Vector3.Normalize(Latéral);
         Vue = Matrix.CreateLookAt(Position, Position + Direction, OrientationVerticale);
         GénérerFrustum();
      }

      protected override void CréerPointDeVue(Vector3 position, Vector3 cible, Vector3 orientation)
      {
         Position = position;
         OrientationVerticale = orientation;
         Direction = cible - Position;
         Latéral = Vector3.Cross(Direction, OrientationVerticale);
         OrientationVerticale = -Vector3.Cross(Direction, Latéral);
         CréerPointDeVue();

      }

      public override void Update(GameTime gameTime)
      {
         float TempsÉcoulé = (float)gameTime.ElapsedGameTime.TotalSeconds;
         TempsÉcouléDepuisMAJ += TempsÉcoulé;
         GestionClavier();
         if (TempsÉcouléDepuisMAJ >= IntervalleMAJ)
         {
            Game.Window.Title = Position.ToString();
          
             
               GérerDéplacement();
               GérerRotation();
               CréerPointDeVue();

            
            TempsÉcouléDepuisMAJ = 0;
         }
         if(GestionInput.EstEnfoncée(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
         {
             Position = new Vector3(-8, 15, 120);
           
             CréerPointDeVue(Position, Cible,Orientation);
         }
         base.Update(gameTime);
      }

      private int GérerTouche(Keys touche)
      {
         return GestionInput.EstEnfoncée(touche) ? 1 : 0;
      }
      private void GérerDéplacement()
      {
         Vector3 nouvellePosition = Position;
         float déplacementDirection = (GérerTouche(Keys.W) - GérerTouche(Keys.S));
         float déplacementLatéral = (GérerTouche(Keys.A) - GérerTouche(Keys.D));

         //if (Position.X > -DIMENSION_TERRAIN / 2 && Position.X < DIMENSION_TERRAIN / 2 && Position.Y > 0 / 2 && Position.Y < DIMENSION_TERRAIN / 2
         //    && Position.Z > -DIMENSION_TERRAIN / 2 && Position.Z < DIMENSION_TERRAIN / 2)
         {

             if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0 || GestionInput.EstEnfoncée(Keys.A) )
             {
                 nouvellePosition -= Latéral * VitesseTranslation;
             }

             if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0 || GestionInput.EstEnfoncée(Keys.D))
             {
                 nouvellePosition += Latéral * VitesseTranslation;
             }

             if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0 || GestionInput.EstEnfoncée(Keys.S))
             {
                 nouvellePosition -= Direction * VitesseTranslation;
                 Cible = Direction;
             }

             if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0 || GestionInput.EstEnfoncée(Keys.W))
             {
                 nouvellePosition += Direction * VitesseTranslation;
                 Cible = Direction;
             }
         }

   
         Position = nouvellePosition;
      }

      private void GérerRotation()
      {
          if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X != 0 || GestionInput.EstEnfoncée(Keys.Left) || GestionInput.EstEnfoncée(Keys.Right))
          {
              GérerLacet();
          }
          if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y != 0 || GestionInput.EstEnfoncée(Keys.Up) || GestionInput.EstEnfoncée(Keys.Down))
          {
              GérerTangage();
          }
         

      }

      private void GérerLacet()
      {
          Matrix matriceLacet;
          if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X > 0 || GestionInput.EstEnfoncée(Keys.Right))
          {
              matriceLacet = Matrix.CreateFromAxisAngle(OrientationVerticale, -DELTA_LACET * VitesseRotation);
          }
          else
          {
              matriceLacet = Matrix.CreateFromAxisAngle(OrientationVerticale, DELTA_LACET * VitesseRotation);
          }

          if (GestionInput.EstEnfoncée(Keys.Left))
          {
              matriceLacet = Matrix.CreateFromAxisAngle(OrientationVerticale, DELTA_LACET * VitesseRotation);
          }
          //Cible = Vector3.Transform(Cible, matriceLacet);
          Direction = Vector3.Transform(Direction, matriceLacet);
          Direction = Vector3.Normalize(Direction);
          Latéral = Vector3.Cross(Direction, OrientationVerticale);
          Latéral = Vector3.Normalize(Latéral);
      }

      private void GérerTangage()
      {
          Matrix matriceTangage;
          if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y < 0  || GestionInput.EstEnfoncée(Keys.Down))
          {
              matriceTangage = Matrix.CreateFromAxisAngle(Latéral, -DELTA_TANGAGE * VitesseRotation);
              
          }
          else
          {
              matriceTangage = Matrix.CreateFromAxisAngle(Latéral, DELTA_TANGAGE * VitesseRotation);
          }
          if (GestionInput.EstEnfoncée(Keys.Up))
          {
              matriceTangage = Matrix.CreateFromAxisAngle(Latéral, DELTA_TANGAGE * VitesseRotation);
          }

          //Cible = Vector3.Transform(Cible, matriceTangage);
          Direction = Vector3.Transform(Direction, matriceTangage);
          Direction = Vector3.Normalize(Direction);
          OrientationVerticale = Vector3.Transform(OrientationVerticale, matriceTangage);
          OrientationVerticale = Vector3.Normalize(OrientationVerticale);
      }

      private void GestionClavier()
      {
          if (GestionInput.EstNouvelleTouche(Keys.Z)) 
         {
            EstEnZoom = !EstEnZoom;
         }
      }
   }
}
