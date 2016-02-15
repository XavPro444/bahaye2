using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
   class Cylindre : PrimitiveDeBaseAnimée
   {
      const int NB_TRIANGLES = 2;
      const int PARTIE_HAUT_ET_BAS = 2;
      const int NB_SOMMETS_PAR_TRIANGLE = 3;
      Vector3 Origine { get; set; }
      float DeltaY { get; set; }
      Vector2 Étendue { get; set; }
      float Rayon { get; set; }
      Vector3[,] PtsSommets { get; set; }
      int NbColonnes { get; set; }
      int NbRangées { get; set; }
      int NbTrianglesTotal { get; set; }
      BasicEffect EffetDeBase { get; set; }
      RessourcesManager<Texture2D> gestionnaireDeTextures;
      Texture2D textureTuile;
      protected VertexPositionTexture[] Sommets { get; set; }
      Vector2[,] PtsTexture { get; set; }
      string NomTexture { get; set; }
      protected BlendState GestionAlpha { get; set; }

      public Cylindre(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector2 étendue, Vector2 charpente, string nomTexture, float intervalleMAJ)
         : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale, intervalleMAJ)
      {
         Étendue = étendue;
         Rayon = étendue.X;
         NbColonnes = (int)charpente.X;
         NbRangées = (int)charpente.Y;
         DeltaY = étendue.Y / (charpente.Y - PARTIE_HAUT_ET_BAS);
         Origine = new Vector3(0, -étendue.Y / 2, 0);
         NomTexture = nomTexture;
      }

      public override void Initialize()
      {
         NbTrianglesTotal = NbColonnes * NbRangées * NB_TRIANGLES + NbColonnes * PARTIE_HAUT_ET_BAS;
         NbSommets = NbTrianglesTotal * NB_SOMMETS_PAR_TRIANGLE;
         CréerTableauSommets();
         CréerTableauPoints();
         base.Initialize();
      }

      void CréerTableauSommets()
      {
         PtsTexture = new Vector2[NbColonnes + 1, NbRangées + 1];
         PtsSommets = new Vector3[NbColonnes + 1, NbRangées - 1]; //-1, texture occupe le haut et le bas, donc 2 rangées de moins dans les ptsSommets
         Sommets = new VertexPositionTexture[NbSommets];
         CréerTableauPointsTexture();
      }

      void CréerTableauPointsTexture()
      {
         for (int j = 0; j < PtsTexture.GetLength(1); ++j)
         {
            for (int i = 0; i < PtsTexture.GetLength(0); ++i)
            {
               PtsTexture[i, j] = new Vector2((float)i / (PtsTexture.GetLength(0) - 1), (float)((PtsTexture.GetLength(1) - 1) - j) / (PtsTexture.GetLength(1) - 1));
            }
         }
      }

      void CréerTableauPoints()
      {
         float angle;
         for (int i = 0; i < PtsSommets.GetLength(1); ++i)
         {
            for (int j = 0; j < PtsSommets.GetLength(0); ++j)
            {
               angle = (float)j / (PtsSommets.GetLength(0) - 1) * MathHelper.TwoPi;
               PtsSommets[j, i] = new Vector3(Origine.X + Rayon * (float)Math.Cos(angle), Origine.Y + i * DeltaY, Origine.Z - Rayon * (float)Math.Sin(angle));
            }
         }
      }

      protected override void InitialiserSommets()
      {
         int NoSommet = -1;
         for (int j = 0; j < PtsSommets.GetLength(1) - 1; ++j)
         {
            for (int i = 0; i < NbColonnes; ++i)
            {
               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, j], PtsTexture[i, j]);
               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, j + 1], PtsTexture[i, j + 1]);
               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i + 1, j], PtsTexture[i + 1, j]);

               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i + 1, j], PtsTexture[i + 1, j]);
               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, j + 1], PtsTexture[i, j + 1]);
               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i + 1, j + 1], PtsTexture[i + 1, j + 1]);
            }
         }
         Vector3 centreBas = new Vector3(Origine.X, PtsSommets[0, 0].Y, Origine.Z);
         for (int i = 0; i < NbColonnes; ++i)
         {
            Sommets[++NoSommet] = new VertexPositionTexture(centreBas, PtsTexture[PtsTexture.GetLength(0) / 2, PtsTexture.GetLength(1) - 1]);
            Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, 0], PtsTexture[i, PtsTexture.GetLength(1) - 2]);
            Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i + 1, 0], PtsTexture[i + 1, PtsTexture.GetLength(1) - 2]);
         }
         Vector3 centreHaut = new Vector3(Origine.X, PtsSommets[0, PtsSommets.GetLength(1) - 1].Y, Origine.Z);
         for (int i = 0; i < NbColonnes; ++i)
         {
            Sommets[++NoSommet] = new VertexPositionTexture(centreHaut, PtsTexture[PtsTexture.GetLength(0) / 2, 0]);
            Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i + 1, PtsSommets.GetLength(1) - 1], PtsTexture[i + 1, 1]);
            Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, PtsSommets.GetLength(1) - 1], PtsTexture[i, 1]);
         }
      }

      protected override void LoadContent()
      {
         gestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
         textureTuile = gestionnaireDeTextures.Find(NomTexture);
         EffetDeBase = new BasicEffect(GraphicsDevice);
         InitialiserParamètresEffetDeBase();
         base.LoadContent();
      }

      void InitialiserParamètresEffetDeBase()
      {
         EffetDeBase.TextureEnabled = true;
         EffetDeBase.Texture = textureTuile;
         GestionAlpha = BlendState.AlphaBlend;
      }

      public override void Draw(GameTime gameTime)
      {
         EffetDeBase.World = GetMonde();
         EffetDeBase.View = CaméraJeu.Vue;
         EffetDeBase.Projection = CaméraJeu.Projection;
         foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
         {
            passeEffet.Apply();
            GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, Sommets, 0, NbTrianglesTotal);
         }
         base.Draw(gameTime);
      }
   }
}
