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
    public class MenuOption : FenêtreDeControle
    {
        const float INTERVALLE_MAJ_STANDARD = 1f / 60f;
        const int DIMENSION_TERRAIN = 256;
        const int NB_ZONES_DIALOGUE = 3; //Cette constante doit valoir 3 au minimum
        string NomImageFond { get; set; }
        Rectangle RectangleDialogue { get; set; }
        //Texture2D ImageDeFond { get; set; }
        BoutonDeCommande BtnDémarrer { get; set; }
        //BoutonDeCommande BtnPause { get; set; }
        BoutonDeCommande BtnQuitter { get; set; }
        RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }

        public MenuOption(Game jeu, string nomImageFond, Rectangle rectangleDialogue)
            : base(jeu, nomImageFond,rectangleDialogue)
        {
            RectangleDialogue = rectangleDialogue;
        }

        public override void Initialize()
        {
            int hauteurBouton = RectangleDialogue.Height / (NB_ZONES_DIALOGUE + 1);

            Vector2 PositionBouton = new Vector2(RectangleDialogue.X + RectangleDialogue.Width / 2f,
                                                 RectangleDialogue.Y + (NB_ZONES_DIALOGUE - 2) * hauteurBouton);
            BtnDémarrer = new BoutonDeCommande(Game, "Démarrer", "", "BoutonRouge", "BoutonBleu", PositionBouton, true, true, GérerPause);

            PositionBouton = new Vector2(RectangleDialogue.X + RectangleDialogue.Width / 2f,
                                                 RectangleDialogue.Y + (NB_ZONES_DIALOGUE - 1) * hauteurBouton);
            //BtnPause = new BoutonDeCommande(Game, "Pause", "", "BoutonRouge", "BoutonBleu", PositionBouton, true, GérerPause);

            //PositionBouton = new Vector2(RectangleDialogue.X + RectangleDialogue.Width / 2f,
            //                                     RectangleDialogue.Y + NB_ZONES_DIALOGUE * hauteurBouton);
            BtnQuitter = new BoutonDeCommande(Game, "Quitter", "", "BoutonRouge", "BoutonBleu", PositionBouton, true, true, Quitter);

            Game.Components.Add(BtnDémarrer);
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
            GestionSprites.Begin();
            base.Draw(gameTime);
            GestionSprites.End();
        }

        public void GérerPause()
        {
            Game.Components.Remove(BtnDémarrer);
            Game.Components.Remove(BtnQuitter);
            Game.Components.Remove(this);
            Game.Components.Add(new Jeu(Game));
        }

        public void Quitter()
        {
            Game.Exit();
        }

        public void DésactiverBoutons()
        {
        }
    }
}
