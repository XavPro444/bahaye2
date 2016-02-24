using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
    public abstract class TuileDiagonal4 : PrimitiveDeBaseAnimée
    {
        const int NB_TRIANGLES = 2;
        protected Vector3[,] PtsSommets { get; private set; }
        Vector3 Origine { get; set; }
        Vector2 Delta { get; set; }
        protected BasicEffect EffetDeBase { get; private set; }


        public TuileDiagonal4(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector2 étendue, float intervalleMAJ)
            : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale, intervalleMAJ)
        {
            Delta = new Vector2(étendue.X, étendue.Y);
            // Origine = new Vector3(-Delta.X / 2, -Delta.Y / 2, 0); //pour centrer la primitive au point (0,0,0)
            Origine = new Vector3(PositionInitiale.X, PositionInitiale.Y, PositionInitiale.Z);
        }

        public override void Initialize()
        {
            NbSommets = NB_TRIANGLES + 2;
            PtsSommets = new Vector3[2, 2];
            CréerTableauSommets();
            CréerTableauPoints();
            base.Initialize();
        }

        protected abstract void CréerTableauSommets();

        protected override void LoadContent()
        {
            EffetDeBase = new BasicEffect(GraphicsDevice);
            InitialiserParamètresEffetDeBase();
            base.LoadContent();
        }

        protected abstract void InitialiserParamètresEffetDeBase();

        private void CréerTableauPoints()
        {
            PtsSommets[0, 0] = new Vector3(Origine.X, Origine.Y, Origine.Z);
            PtsSommets[1, 0] = new Vector3(Origine.X + Delta.X, Origine.Y, Origine.Z - 25);
            PtsSommets[0, 1] = new Vector3(Origine.X, Origine.Y + Delta.Y, Origine.Z);
            PtsSommets[1, 1] = new Vector3(Origine.X + Delta.X, Origine.Y + Delta.Y, Origine.Z - 25);
        }

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