using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
    public abstract class TuileVertical : Tuile
    {
        const int NB_TRIANGLES = 2;

        int Indice { get; set; }


        public TuileVertical(Game jeu, int indice, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector2 étendue, float intervalleMAJ)
            : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale, étendue, intervalleMAJ)
        {
            Indice = indice;
        }

        public override void Initialize()
        {
            NbSommets = NB_TRIANGLES + 2;
            CréerTableauSommets();
            CréerTableauPoints();
            base.Initialize();
            CréationBoundingBoxes();
        }

        protected override void LoadContent()
        {
            InitialiserParamètresEffetDeBase();
            base.LoadContent();
        }
        private void CréerTableauPoints()
        {
            if(Indice ==1)
            {
                PtsSommets[0, 0] = new Vector3(Origine.X, Origine.Y, Origine.Z);
                PtsSommets[1, 0] = new Vector3(Origine.X + Delta.X, Origine.Y, Origine.Z);
                PtsSommets[0, 1] = new Vector3(Origine.X, Origine.Y + Delta.Y, Origine.Z);
                PtsSommets[1, 1] = new Vector3(Origine.X + Delta.X, Origine.Y + Delta.Y, Origine.Z);
            }
            else
            {
                if(Indice ==2)
                {
                    PtsSommets[0, 0] = new Vector3(Origine.X, Origine.Y + Delta.Y, Origine.Z);
                    PtsSommets[1, 0] = new Vector3(Origine.X + Delta.X, Origine.Y + Delta.Y, Origine.Z);
                    PtsSommets[0, 1] = new Vector3(Origine.X, Origine.Y, Origine.Z);
                    PtsSommets[1, 1] = new Vector3(Origine.X + Delta.X, Origine.Y, Origine.Z);
                }
                else
                {
                    if(Indice ==3)
                    {
                        PtsSommets[0, 0] = new Vector3(Origine.X, Origine.Y, Origine.Z);
                        PtsSommets[1, 0] = new Vector3(Origine.X, Origine.Y, Origine.Z + Delta.X);
                        PtsSommets[0, 1] = new Vector3(Origine.X, Origine.Y + Delta.Y, Origine.Z);
                        PtsSommets[1, 1] = new Vector3(Origine.X, Origine.Y + Delta.Y, Origine.Z + Delta.X);
                    }
                    else 
                    {
                        PtsSommets[0, 0] = new Vector3(Origine.X, Origine.Y, Origine.Z + Delta.X);
                        PtsSommets[1, 0] = new Vector3(Origine.X, Origine.Y, Origine.Z);
                        PtsSommets[0, 1] = new Vector3(Origine.X, Origine.Y + Delta.Y, Origine.Z + Delta.X);
                        PtsSommets[1, 1] = new Vector3(Origine.X, Origine.Y + Delta.Y, Origine.Z);
                    }
                }
            }

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
    }
}

