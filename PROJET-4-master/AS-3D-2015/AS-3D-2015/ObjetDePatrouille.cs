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
   public class ObjetDePatrouille : ObjetDeBase
   {
      Vector3 RotationInitiale { get; set; }
      Vector3 PositionInitiale { get; set; }
      Vector3 PositionAxe { get; set; }
      int NbPtsPatrouille { get; set; }
      float VitesseAngulaire { get; set; }
      float IntervalleMAJ { get; set; }
      float TempsÉcouléDepuisMAJ { get; set; }
      float AnglePoint { get; set; }
      float NbDéplacementEntreChaquePoint { get; set; }
      Vector3[] PositionSommet { get; set; }
      float Rayon { get; set; }
      int Index { get; set; }
      Vector3 MiniVariation { get; set; }
      Vector3 Déplacement { get; set; }

      public ObjetDePatrouille(Game jeu, String nomModèle, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, 
           Vector3 positionAxe, int nbPtsPatrouille, float vitesse, float intervalleMAJ)
         : base(jeu,nomModèle,échelleInitiale,rotationInitiale,positionInitiale)
      {
         RotationInitiale = rotationInitiale;
         PositionInitiale = positionInitiale;
         Position = positionInitiale + positionAxe;
         PositionAxe = positionAxe;
         NbPtsPatrouille = nbPtsPatrouille;
         VitesseAngulaire = vitesse;
         IntervalleMAJ = intervalleMAJ;
      }

      public override void Initialize()
      {
         Rotation = RotationInitiale;
         Index = 1;
         TempsÉcouléDepuisMAJ = 0;
         AnglePoint = MathHelper.TwoPi / NbPtsPatrouille;
         Rayon = (float)Math.Sqrt(Math.Pow(PositionInitiale.X, 2) + Math.Pow(PositionInitiale.Z, 2));
         PositionSommet = new Vector3[NbPtsPatrouille];
         for (int i = 0; i < NbPtsPatrouille; ++i)
         {
            PositionSommet[i] = new Vector3(PositionAxe.X + (float)Math.Cos(AnglePoint * i) * Rayon,
                                PositionInitiale.Y, PositionAxe.Z + (float)Math.Sin(AnglePoint * i) * Rayon);
         }
         Déplacement = (PositionSommet[Index] - Position) / VitesseAngulaire;
         base.Initialize();
      }

      public override void Update(GameTime gameTime)
      {
         float TempsÉcoulé = (float)gameTime.ElapsedGameTime.TotalSeconds;
         TempsÉcouléDepuisMAJ += TempsÉcoulé;
         if (TempsÉcouléDepuisMAJ >= IntervalleMAJ)
         {
            EffectuerDéplacement();
            Monde = GetMonde();
            TempsÉcouléDepuisMAJ = 0;
         }
         base.Update(gameTime);
      }

      void EffectuerDéplacement()
      {
         if(VérifierArrivéDestination())
         {
            Index = (Index == NbPtsPatrouille - 1) ? 0 : ++Index;
            Déplacement = (PositionSommet[Index] - Position) /  VitesseAngulaire;
         }
         else
         {
            Position += Déplacement;
         }
         MiniVariation = PositionSommet[Index == NbPtsPatrouille - 1 ? 0 : Index + 1] - Position;
         Rotation = new Vector3(Rotation.X, RotationInitiale.Y + MathHelper.PiOver2 - (float)Math.Atan2(MiniVariation.Z, MiniVariation.X), Rotation.Z);
      }

      bool VérifierArrivéDestination()
      {
         bool temp;
         if(Déplacement.X < 0)
         {
             if (Déplacement.Z > 0)
             {
                 temp = Position.X <= PositionSommet[Index].X && Position.Z >= PositionSommet[Index].Z;
             }
             else
             {
                 temp = Position.X <= PositionSommet[Index].X && Position.Z <= PositionSommet[Index].Z;
             }
         }
         else
         {
             if (Déplacement.Z > 0)
             {
                 temp = Position.X >= PositionSommet[Index].X && Position.Z >= PositionSommet[Index].Z;
             }
             else
             {
                 temp = Position.X >= PositionSommet[Index].X && Position.Z <= PositionSommet[Index].Z;
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
