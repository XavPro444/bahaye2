using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
   public abstract class Tuile : PrimitiveDeBaseAnimée
   {
      const int NB_TRIANGLES = 2;
      protected Vector3[,] PtsSommets { get; set; }
      protected Vector3 Origine { get; private set; }
      protected Vector2 Delta { get; private set; }
      protected BasicEffect EffetDeBase { get; set; }
      public BoundingBox BBTuile { get; protected set; }


      public Tuile(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector2 étendue, float intervalleMAJ)
         : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale, intervalleMAJ)
      {
         Delta = new Vector2(étendue.X, étendue.Y);
         Origine = new Vector3(PositionInitiale.X, PositionInitiale.Y, PositionInitiale.Z);
      }

      public override void Initialize()
      {
         NbSommets = NB_TRIANGLES + 2;
         PtsSommets = new Vector3[2, 2];
         CréerTableauSommets();
         CréerTableauPoints();
         CréationBoundingBoxes();
         base.Initialize();
      }
       protected void CréationBoundingBoxes()
      {
          BBTuile = new BoundingBox(PtsSommets[0, 0], PtsSommets[1, 1]);
      }
      protected abstract void CréerTableauSommets();
      protected abstract void CréerTableauPoints();

      protected override void LoadContent()
      {
         EffetDeBase = new BasicEffect(GraphicsDevice);
         InitialiserParamètresEffetDeBase();
         base.LoadContent();
      }
      protected abstract void InitialiserParamètresEffetDeBase();
      public override void Draw(GameTime gameTime)
      {
         EffetDeBase.World = GetMonde();
         EffetDeBase.View = CaméraJeu.Vue;
         EffetDeBase.Projection = CaméraJeu.Projection;
         foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
         {
            passeEffet.Apply();
            DessinerTriangleStrip();
         }
         base.Draw(gameTime);
      }

      protected abstract void DessinerTriangleStrip();
   }
}

