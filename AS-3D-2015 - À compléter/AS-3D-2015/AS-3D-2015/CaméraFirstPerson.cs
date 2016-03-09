using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace AtelierXNA
{
    public class CaméraFirstPerson: Caméra
    {
        const float INTERVALLE_MAJ_STANDARD = 1f / 60f;
        const float ACCÉLÉRATION = 0.001f;
        const int DIMENSION_TERRAIN = 250;
        const float MAX_COULEUR = 255f;
        const float VITESSE_INITIALE_ROTATION = 1;
        const float VITESSE_INITIALE_TRANSLATION = 0.5f;
        const float DELTA_LACET = MathHelper.Pi / 180; // 1 degré à la fois
        const float DELTA_TANGAGE = MathHelper.Pi / 180; // 1 degré à la fois
        const float DELTA_ROULIS = MathHelper.Pi / 180; // 1 degré à la fois
        const float RAYON_COLLISION = 1f;

        Vector3 Direction { get; set; }
        Vector3 Latéral { get; set; }
        Texture2D CarteTerrain { get; set; }
        RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
        string NomCarterTerrain { get; set; }
        Vector3 Orientation { get; set; }
        Color[] DataTexture { get; set; }
        float VitesseTranslation { get; set; }
        float VitesseRotation { get; set; }
        Vector2 Delta { get; set; }
        Vector3 Étendue { get; set; }
        float IntervalleMAJ { get; set; }
        float TempsÉcouléDepuisMAJ { get; set; }
        float i { get; set; }
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

        public CaméraFirstPerson(Game jeu, Vector3 positionCaméra, Vector3 cible, Vector3 orientation, float intervalleMAJ, string nomcarteterrain)
            : base(jeu)
        {
            NomCarterTerrain = nomcarteterrain;
            IntervalleMAJ = intervalleMAJ;
            CréerVolumeDeVisualisation(OUVERTURE_OBJECTIF, DISTANCE_PLAN_RAPPROCHÉ, DISTANCE_PLAN_ÉLOIGNÉ);
            CréerPointDeVue(positionCaméra, cible, orientation);
            Cible = cible;
            Orientation = orientation;
            EstEnZoom = false;
        }

        public override void Initialize()
        {
            i = 4;
            //InitialiserDonnéesCarte();
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



                Position = new Vector3(Position.X, 4, Position.Z);
                TempsÉcouléDepuisMAJ = 0;
            }
            ++i;
            if (GestionInput.EstEnfoncée(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
            {
                Position = new Vector3(-8, 3.2f, 120);

                CréerPointDeVue(Position, Cible, Orientation);
            }
            base.Update(gameTime);
        }
        //void InitialiserDonnéesCarte()
        //{
        //    CarteTerrain = GestionnaireDeTextures.Find(NomCarterTerrain);
        //    Delta = new Vector2(Étendue.X / CarteTerrain.Width, Étendue.Z / CarteTerrain.Height);
        //    DataTexture = new Color[(int)(CarteTerrain.Width * CarteTerrain.Height)];
        //    CarteTerrain.GetData<Color>(DataTexture);
        //}

        //float HauteurPourPts(int posX, int posY)
        //{
        //    return DataTexture[DataTexture.GetLength(0) - CarteTerrain.Width * (posY + 1) + posX].B / MAX_COULEUR * Étendue.Y;
        //}

        private int GérerTouche(Keys touche)
        {
            return GestionInput.EstEnfoncée(touche) ? 1 : 0;
        }
        public float gethauteur
        {
            get 
            {
                return 2;
            }
        }
       public bool InView(BoundingSphere boundingSphere)
        {
            return new BoundingFrustum(Vue * Projection).Intersects(boundingSphere);
        }
        #region déplacement
        private void GérerDéplacement()
        {
            Vector3 nouvellePosition = Position;
           // float déplacementDirection = (GérerTouche(Keys.W) - GérerTouche(Keys.S));
            //float déplacementLatéral = (GérerTouche(Keys.A) - GérerTouche(Keys.D));

            {

                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0 || GestionInput.EstEnfoncée(Keys.A))
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
                Cible = Vector3.Transform(Cible, matriceLacet);
                Direction = Vector3.Transform(Direction, matriceLacet);
                Direction = Vector3.Normalize(Direction);
                Latéral = Vector3.Cross(Direction, OrientationVerticale);
                Latéral = Vector3.Normalize(Latéral);
            }
            else
            {
                matriceLacet = Matrix.CreateFromAxisAngle(OrientationVerticale, DELTA_LACET * VitesseRotation);
                Cible = Vector3.Transform(Cible, matriceLacet);
                Direction = Vector3.Transform(Direction, matriceLacet);
                Direction = Vector3.Normalize(Direction);
                Latéral = Vector3.Cross(Direction, OrientationVerticale);
                Latéral = Vector3.Normalize(Latéral);
            }

            if (GestionInput.EstEnfoncée(Keys.Left))
            {
                matriceLacet = Matrix.CreateFromAxisAngle(OrientationVerticale, DELTA_LACET * VitesseRotation);
                Cible = Vector3.Transform(Cible, matriceLacet);
                Direction = Vector3.Transform(Direction, matriceLacet);
                Direction = Vector3.Normalize(Direction);
                Latéral = Vector3.Cross(Direction, OrientationVerticale);
                Latéral = Vector3.Normalize(Latéral);
            }

        }

        private void GérerTangage()
        {
            Matrix matriceTangage;
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y < 0 || GestionInput.EstEnfoncée(Keys.Down))
            {
                matriceTangage = Matrix.CreateFromAxisAngle(Latéral, -DELTA_TANGAGE * VitesseRotation);
                Direction = Vector3.Transform(Direction, matriceTangage);
                Direction = Vector3.Normalize(Direction);

            }
            else
            {
                matriceTangage = Matrix.CreateFromAxisAngle(Latéral, DELTA_TANGAGE * VitesseRotation);
                Direction = Vector3.Transform(Direction, matriceTangage);
                Direction = Vector3.Normalize(Direction);
            }

            if (GestionInput.EstEnfoncée(Keys.Up))
            {

                matriceTangage = Matrix.CreateFromAxisAngle(Latéral, DELTA_TANGAGE * VitesseRotation);
                Direction = Vector3.Transform(Direction, matriceTangage);
                Direction = Vector3.Normalize(Direction);

            }

            //Cible = Vector3.Transform(Cible, matriceTangage);

            //OrientationVerticale = Vector3.Transform(OrientationVerticale, matriceTangage);
            //OrientationVerticale = Vector3.Normalize(OrientationVerticale);
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
        #endregion
