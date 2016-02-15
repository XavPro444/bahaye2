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
   public class ObjetDePatrouille2 : ObjetDeBase
   {
      Vector3 PositionAxe { get; set; }
      int NbPtsPatrouille { get; set; }
      float VitesseAngulaire { get; set; }
      float IntervalleMAJ { get; set; }
      float TempsÉcouléDepuisMAJ { get; set; }
      //float[] SommetsPoints { get; set; }
      float AnglePoint { get; set; }
      float NbDéplacementEntreChaquePoint { get; set; }
      Vector3[] PositionSommet { get; set; }
      float Rayon { get; set; }
      int Index { get; set; }
      //Vector3 Origine { get; set; }
      Vector3 Déplacement { get; set; }

      public ObjetDePatrouille2(Game jeu, String nomModèle, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, 
           Vector3 positionAxe, int nbPtsPatrouille, float vitesse, float intervalleMAJ)
         : base(jeu,nomModèle,échelleInitiale,rotationInitiale,positionInitiale)
      {
         Position = new Vector3(positionAxe.X + positionInitiale.X, positionAxe.Y, positionAxe.Z + positionInitiale.Z);
         PositionAxe = positionAxe;
         NbPtsPatrouille = nbPtsPatrouille;
         VitesseAngulaire = vitesse;
         IntervalleMAJ = intervalleMAJ;
      }

      public override void Initialize()
      {
         Index = 0;
         TempsÉcouléDepuisMAJ = 0;
         AnglePoint = MathHelper.TwoPi / NbPtsPatrouille;
         Rayon = (float)Math.Sqrt(Math.Pow(Position.X - PositionAxe.X, 2) + Math.Pow(Position.Z - PositionAxe.Z, 2));
         PositionSommet = new Vector3[NbPtsPatrouille];
         for (int i = 0; i < NbPtsPatrouille; ++i)
         {
            //SommetsPoints[i] = AnglePoint * i;
            PositionSommet[i] = new Vector3(PositionAxe.X + ((float)Math.Cos(AnglePoint * i) * Rayon), PositionAxe.Y, PositionAxe.Z + ((float)Math.Sin(AnglePoint * i) * Rayon));
         }
         Déplacement = (PositionSommet[Index+1] - Position) / (IntervalleMAJ * VitesseAngulaire);
         base.Initialize();
      }

      public override void Update(GameTime gameTime)
      {
         float TempsÉcoulé = (float)gameTime.ElapsedGameTime.TotalSeconds;
         TempsÉcouléDepuisMAJ += TempsÉcoulé;
         if (TempsÉcouléDepuisMAJ >= IntervalleMAJ)
         {
            EffectuerDéplacement();
            TempsÉcouléDepuisMAJ = 0;
         }
         base.Update(gameTime);
      }

      void EffectuerDéplacement()
      {

         if(VérifierArrivéDestination())
         {
            Position = PositionSommet[Index++];
            Déplacement = (PositionSommet[Index] - Position) / (IntervalleMAJ * VitesseAngulaire);
         }
         else
         {
            Position += Déplacement;
         }
      }

      bool VérifierArrivéDestination()
      {
         bool temp;
         if(Déplacement.X < 0)
         {
            if(Déplacement.Z > 0)
            {
                temp = PositionSommet[Index].X <= PositionSommet[Index + 1].X && PositionSommet[Index].Z >= PositionSommet[Index + 1].Z;
            }
            else
            {
                temp =  PositionSommet[Index].X <= PositionSommet[Index + 1].X && PositionSommet[Index].Z <= PositionSommet[Index + 1].Z;
            }

         }
         else
         {
            if (Déplacement.Z > 0)
            {
               temp = PositionSommet[Index].X >= PositionSommet[Index + 1].X && PositionSommet[Index].Z >= PositionSommet[Index + 1].Z;
            }
            else
            {
               temp = PositionSommet[Index].X >= PositionSommet[Index + 1].X && PositionSommet[Index].Z <= PositionSommet[Index + 1].Z;
            }
         }
         return temp;
      }
      public override Matrix GetMonde()
      {
         Monde = Matrix.Identity;
         Monde *= Matrix.CreateScale(Échelle);
         Monde *= Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z);
         Monde *= Matrix.CreateTranslation(Position);
         return base.GetMonde();
      }
   }
}
