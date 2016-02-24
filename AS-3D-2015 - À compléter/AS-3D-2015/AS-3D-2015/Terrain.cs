using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
   public class Terrain : PrimitiveDeBase
   {
      const int NB_TRIANGLES_PAR_TUILE = 2;
      const int NB_SOMMETS_PAR_TRIANGLE = 3;
      const int NB_SOMMETS_PAR_TUILE = 4;
      const float MAX_COULEUR = 255f;

      Vector3 Étendue { get; set; }
      string NomCarteTerrain { get; set; }
      string NomTextureTerrain { get; set; }
      int NbNiveauTexture { get; set; }
      int nbTriangles;
 
      BasicEffect EffetDeBase { get; set; }
      RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
       Texture2D CarteTerrain { get;  set; }
      Texture2D TextureTerrain { get; set; }
      Vector3 Origine { get; set; }
      Color[] DataTexture { get; set; }
      Vector2[,] PtsTexture { get; set; }
      Vector3[,] PtsSommets { get; set; }
      VertexPositionTexture[] Sommets { get; set; }
      Vector2 Delta { get; set; }


      public Terrain(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale,
                     Vector3 étendue, string nomCarteTerrain, string nomTextureTerrain, int nbNiveauxTexture, float intervalleMAJ)
         : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale)
      {
         Étendue = étendue;
         NomCarteTerrain = nomCarteTerrain;
         NomTextureTerrain = nomTextureTerrain;
         NbNiveauTexture = nbNiveauxTexture;
      }

      public override void Initialize()
      {
         GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
         InitialiserDonnéesCarte();
         InitialiserDonnéesTexture();
         Origine = new Vector3(-Étendue.X / 2, 0, Étendue.Z / 2); //pour centrer la primitive au point (0,0,0)
         AllouerTableaux();
         CréerTableauPoints();
         base.Initialize();
      }

      void InitialiserDonnéesCarte()
      {
         CarteTerrain = GestionnaireDeTextures.Find(NomCarteTerrain);
         Delta = new Vector2(Étendue.X / CarteTerrain.Width, Étendue.Z / CarteTerrain.Height);
         DataTexture = new Color[(int)(CarteTerrain.Width * CarteTerrain.Height)];
         CarteTerrain.GetData<Color>(DataTexture);
      }


      void InitialiserDonnéesTexture()
      {
         TextureTerrain = GestionnaireDeTextures.Find(NomTextureTerrain);
         PtsTexture = new Vector2[1 + 1, NbNiveauTexture + 1];
         CréerTableauPointsTexture();
      }

      void CréerTableauPointsTexture()
      {
         for (int j = 0; j <= NbNiveauTexture; ++j)
         {
            for (int i = 0; i <= 1; ++i)
            {
               PtsTexture[i, j] = new Vector2(i, (float)(NbNiveauTexture - j) / NbNiveauTexture);
            }
         }
      }

      void AllouerTableaux()
      {
         nbTriangles = (int)((CarteTerrain.Width - 1) * (CarteTerrain.Height - 1) * NB_TRIANGLES_PAR_TUILE);
         PtsSommets = new Vector3[(int)CarteTerrain.Width, (int)CarteTerrain.Height];
         Sommets = new VertexPositionTexture[nbTriangles * NB_SOMMETS_PAR_TRIANGLE];
      }

      protected override void LoadContent()
      {
         base.LoadContent();
         EffetDeBase = new BasicEffect(GraphicsDevice);
         InitialiserParamètresEffetDeBase();
      }

      void InitialiserParamètresEffetDeBase()
      {
         EffetDeBase.TextureEnabled = true;
         EffetDeBase.Texture = TextureTerrain;
      }

      private void CréerTableauPoints()
      {
         for (int i = 0; i < CarteTerrain.Height; ++i)
         {
            for (int j = 0; j < CarteTerrain.Width; ++j)
            {
               PtsSommets[j, i] = new Vector3(Origine.X + j * Delta.X, Origine.Y + HauteurPourPts(j, i), Origine.Z - i * Delta.Y);
            }
         }
      }

      float HauteurPourPts(int posX, int posY)
      {
         return DataTexture[DataTexture.GetLength(0) - CarteTerrain.Width * (posY + 1) + posX].B / MAX_COULEUR * Étendue.Y;
      }

      protected override void InitialiserSommets()
      {
         float hauteurMoyenne = 0;
         int ptsDeDépart = 0;
         int NoSommet = -1;
         for (int j = 0; j < CarteTerrain.Height - 1; ++j)
         {
            for (int i = 0; i < CarteTerrain.Width - 1; ++i)
            {
               hauteurMoyenne = (PtsSommets[i, j].Y + PtsSommets[i, j + 1].Y + PtsSommets[i + 1, j].Y) / 3f;
               ptsDeDépart = CalculerPtsDeDépart(hauteurMoyenne);
               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, j], PtsTexture[0, ptsDeDépart]);
               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, j + 1], PtsTexture[0, ptsDeDépart + 1]);
               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i + 1, j], PtsTexture[1, ptsDeDépart]);

               hauteurMoyenne = (PtsSommets[i + 1, j].Y + PtsSommets[i, j + 1].Y + PtsSommets[i + 1, j + 1].Y) / 3f;
               ptsDeDépart = CalculerPtsDeDépart(hauteurMoyenne);
               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i + 1, j], PtsTexture[1, ptsDeDépart]);
               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, j + 1], PtsTexture[0, ptsDeDépart + 1]);
               Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i + 1, j + 1], PtsTexture[1, ptsDeDépart + 1]);
            }
         }
      }

      int CalculerPtsDeDépart(float hauteurMoyenne)
      {
         int ptsDeDépart = 0;
         float rapport = hauteurMoyenne / Étendue.Y;
         for (int i = 1; i <= NbNiveauTexture; ++i)
         {
            if(rapport >= PtsTexture[0, i].Y)
            {
               ptsDeDépart = i - 1;
               i = NbNiveauTexture;
            }
         }

         return ptsDeDépart;
      }
      //public float gethauteur(float ptsx,float ptsxy, float ptsz)
      //{
         
      //}
      public override void Draw(GameTime gameTime)
      {
         EffetDeBase.World = GetMonde();
         EffetDeBase.View = CaméraJeu.Vue;
         EffetDeBase.Projection = CaméraJeu.Projection;
         foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
         {
            passeEffet.Apply();
            GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, Sommets, 0, nbTriangles);
         }
         base.Draw(gameTime);
      }
   }
}
