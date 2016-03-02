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
   public class CaméraSubjective : Caméra
   {
      const float INTERVALLE_MAJ_STANDARD = 1f / 60f;
      const float ACCÉLÉRATION = 0.001f;
      const float VITESSE_INITIALE_ROTATION = 5f;
      const float VITESSE_INITIALE_TRANSLATION = 0.5f;
      const float DELTA_LACET = MathHelper.Pi / 180; // 1 degré à la fois
      const float DELTA_TANGAGE = MathHelper.Pi / 180; // 1 degré à la fois
      const float DELTA_ROULIS = MathHelper.Pi / 180; // 1 degré à la fois
      const float RAYON_COLLISION = 1f;

      Vector3 Direction { get; set; }
      Vector3 Latéral { get; set; }
      float VitesseTranslation { get; set; }
      float VitesseRotation { get; set; }

      float IntervalleMAJ { get; set; }
      float TempsÉcouléDepuisMAJ { get; set; }
      InputManager GestionInput { get; set; }
      BoundingBox BBCaméra { get; set; }
      //Vector3 position;
      // public Vector3 Position
      //{
      //    get { return position; }
      //    private set
      //    {
      //        Position = position;
      //    }
      //}
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
         EstEnZoom = false;
      }

      public override void Initialize()
      {
         VitesseRotation = VITESSE_INITIALE_ROTATION;
         VitesseTranslation = VITESSE_INITIALE_TRANSLATION;
         TempsÉcouléDepuisMAJ = 0;
         base.Initialize();
         BBCaméra = new BoundingBox(new Vector3(Position.X - 1, Position.Y - 1f, Position.Z - 1), new Vector3(Position.X + 1, Position.Y + 1f, Position.Z + 1));
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

               GérerAccélération();
               GérerDéplacement();
               GérerRotation();
               CréerPointDeVue();
            
            TempsÉcouléDepuisMAJ = 0;
         }
         base.Update(gameTime);
      }

      private int GérerTouche(Keys touche)
      {
         return GestionInput.EstEnfoncée(touche) ? 1 : 0;
      }

      private void GérerAccélération()
      {
         int valAccélération = (GérerTouche(Keys.Subtract) + GérerTouche(Keys.OemMinus)) - (GérerTouche(Keys.Add) + GérerTouche(Keys.OemPlus));
         if (valAccélération != 0)
         {
            IntervalleMAJ += ACCÉLÉRATION * valAccélération;
            IntervalleMAJ = MathHelper.Max(INTERVALLE_MAJ_STANDARD, IntervalleMAJ);
         }
      }

      private void GérerDéplacement()
      {

         Vector3 nouvellePosition = Position;
         float déplacementDirection = (GérerTouche(Keys.W) - GérerTouche(Keys.S));
         float déplacementLatéral = (GérerTouche(Keys.A) - GérerTouche(Keys.D));

         if (déplacementDirection != 0)
         {
            if (déplacementDirection > 0)
            {
               nouvellePosition += Direction * VitesseTranslation;
            }
            else
            {
               nouvellePosition -= Direction * VitesseTranslation;
            }

         }

         if (déplacementLatéral != 0)
         {
            if (déplacementLatéral < 0)
            {
               nouvellePosition += Latéral * VitesseTranslation;
            }
            else
            {
               nouvellePosition -= Latéral * VitesseTranslation;
            }

         }
         foreach (Tuile tuile in Game.Components.Where(x => x is Tuile))
         {
             if(tuile.BBTuile.Intersects(BBCaméra))
             {
                 Position = Position;
             }
             else
             {
                 Position = nouvellePosition;
             }
         }
      }

      private void GérerRotation()
      {
         if (GestionInput.EstEnfoncée(Keys.Right) || GestionInput.EstEnfoncée(Keys.Left))
         {
            GérerLacet();
         }
         if (GestionInput.EstEnfoncée(Keys.Up) || GestionInput.EstEnfoncée(Keys.Down))
         {
            GérerTangage();
         }
         if (GestionInput.EstEnfoncée(Keys.PageUp) || GestionInput.EstEnfoncée(Keys.PageDown))
         {
            GérerRoulis();
         }

      }

      private void GérerLacet()
      {
         Matrix matriceLacet;
         if (GestionInput.EstEnfoncée(Keys.Right))
         {
            matriceLacet = Matrix.CreateFromAxisAngle(OrientationVerticale, -DELTA_LACET * VitesseRotation);
         }
         else
         {
            matriceLacet = Matrix.CreateFromAxisAngle(OrientationVerticale, DELTA_LACET * VitesseRotation);
         }
         Direction = Vector3.Transform(Direction, matriceLacet);
         Direction = Vector3.Normalize(Direction);
         Latéral = Vector3.Cross(Direction, OrientationVerticale);
         Latéral = Vector3.Normalize(Latéral);
      }

      private void GérerTangage()
      {
         Matrix matriceTangage;
         if (GestionInput.EstEnfoncée(Keys.Up))
         {
            matriceTangage = Matrix.CreateFromAxisAngle(Latéral, -DELTA_TANGAGE * VitesseRotation);
         }
         else
         {
            matriceTangage = Matrix.CreateFromAxisAngle(Latéral, DELTA_TANGAGE * VitesseRotation);
         }
         Direction = Vector3.Transform(Direction, matriceTangage);
         Direction = Vector3.Normalize(Direction);
         OrientationVerticale = Vector3.Transform(OrientationVerticale, matriceTangage);
         OrientationVerticale = Vector3.Normalize(OrientationVerticale);
      }

      private void GérerRoulis()
      {
         Matrix matriceRoulis;
         if (GestionInput.EstEnfoncée(Keys.PageUp))
         {
            matriceRoulis = Matrix.CreateFromAxisAngle(Direction, DELTA_ROULIS * VitesseRotation);
         }
         else
         {
            matriceRoulis = Matrix.CreateFromAxisAngle(Direction, -DELTA_ROULIS * VitesseRotation);
         }
            OrientationVerticale = Vector3.Transform(OrientationVerticale, matriceRoulis);
            OrientationVerticale = Vector3.Normalize(OrientationVerticale);
            Latéral = Vector3.Cross(Direction, OrientationVerticale);
            Latéral = Vector3.Normalize(Latéral);
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
