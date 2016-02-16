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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MenuJeu :  FenêtreDeControle
    {
        const float INTERVALLE_MAJ_STANDARD = 1f / 60f;
        const int DIMENSION_TERRAIN = 256;
        const int NB_ZONES_DIALOGUE = 3; //Cette constante doit valoir 3 au minimum
        string NomImageFond { get; set; }
        Rectangle RectangleDialogue { get; set; }
        //Texture2D ImageDeFond { get; set; }
        BoutonDeCommande BtnModeCaméra { get; set; }
        //BoutonDeCommande BtnPause { get; set; }
        BoutonDeCommande BtnQuitter { get; set; }
        RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }

        public MenuJeu(Game jeu, string nomImageFond, Rectangle rectangleDialogue)
            : base(jeu, nomImageFond,rectangleDialogue)
        {
            RectangleDialogue = rectangleDialogue;
        }

        public override void Initialize()
        {
            int hauteurBouton = RectangleDialogue.Height / (NB_ZONES_DIALOGUE + 1);

            Vector2 PositionBouton = new Vector2(RectangleDialogue.X + RectangleDialogue.Width / 2f,
                                                 RectangleDialogue.Y + (NB_ZONES_DIALOGUE - 2) * hauteurBouton);
            BtnModeCaméra = new BoutonDeCommande(Game, "Mode GOD", "", "BoutonRouge", "BoutonBleu", PositionBouton, true, GérerCaméra);

            PositionBouton = new Vector2(RectangleDialogue.X + RectangleDialogue.Width / 2f,
                                                 RectangleDialogue.Y + (NB_ZONES_DIALOGUE - 1) * hauteurBouton);
            //BtnPause = new BoutonDeCommande(Game, "Pause", "", "BoutonRouge", "BoutonBleu", PositionBouton, true, GérerPause);

            //PositionBouton = new Vector2(RectangleDialogue.X + RectangleDialogue.Width / 2f,
            //                                     RectangleDialogue.Y + NB_ZONES_DIALOGUE * hauteurBouton);
            BtnQuitter = new BoutonDeCommande(Game, "Quitter", "", "BoutonRouge", "BoutonBleu", PositionBouton, true, Quitter);

            Game.Components.Add(BtnModeCaméra);
            //Game.Components.Add(BtnPause);
            Game.Components.Add(BtnQuitter);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        public void GérerCaméra()
        {

        }
        public void GérerPause()
        {
            //BtnDémarrer.EstActif = !BtnDémarrer.EstActif;
            //BtnPause.EstActif = !BtnPause.EstActif;
            //foreach (IActivable composant in Game.Components.Where(composant => composant is IActivable))
            //{
            //    composant.ModifierActivation();
            //}
            

         //LignePArking    
            //Game.Components.Clear();
            Game.Components.Remove(BtnModeCaméra);
            Game.Components.Remove(BtnQuitter);
            Game.Components.Remove(this);

            Game.Components.Add(new Terrain(Game, 1f, Vector3.Zero, Vector3.Zero, new Vector3(DIMENSION_TERRAIN, 3, DIMENSION_TERRAIN), "LionelEssai4", "TextureEssai2", 3, INTERVALLE_MAJ_STANDARD));

        }

        public void Quitter()
        {
            Game.Exit();
        }

        public void DésactiverBoutons()
        {
            BtnModeCaméra.EstActif = false;
            //BtnPause.EstActif = false;
        }
    }
}
