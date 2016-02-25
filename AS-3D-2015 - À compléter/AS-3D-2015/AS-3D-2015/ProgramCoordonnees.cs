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
using System.IO;


namespace AtelierXNA
{

    public class ProgramCoordonnees : Microsoft.Xna.Framework.GameComponent
    {
        InputManager GestionInput { get; set; }
        //CaméraSubjective CaméraSub { get; set; }
        //Vector3 Position { get; set; }
        string Extension { get;set;}
        string FichierSortie { get; set; }
        public ProgramCoordonnees(Game game, string extension, string fichierSortie) 
            : base(game)
        {
            Extension = extension;
            FichierSortie = fichierSortie;
        }

        public override void Initialize()
        {
            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            if (GestionInput.EstNouvelleTouche(Keys.Space))
            {
                
                string nomFichierÉcriture = "../../" + FichierSortie + Extension;
                StreamWriter fSortie = new StreamWriter(nomFichierÉcriture,true); // Ouverture du fichier en "écriture"
                fSortie.WriteLine(Game.Window.Title.ToString()); // Écriture à la suite du fichier de sortie
                fSortie.Close();
            }
            base.Update(gameTime);
        }
    }
}
