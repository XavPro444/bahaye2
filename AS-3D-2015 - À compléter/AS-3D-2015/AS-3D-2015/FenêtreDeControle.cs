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
    public class FenêtreDeControle : Microsoft.Xna.Framework.DrawableGameComponent
    {
      protected SpriteBatch GestionSprites { get; private set; }
      RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
      string NomImage { get; set; } 
      Rectangle ZoneAffichage { get; set; }
      protected Texture2D ImageDeFond { get; private set; }

      public FenêtreDeControle(Game jeu, string nomImage, Rectangle zoneAffichable)
         : base(jeu)
      {
         NomImage = nomImage;
         ZoneAffichage = zoneAffichable;
      }

      public override void Initialize()
      {
         base.Initialize();
      }

      protected override void LoadContent()
      {
         GestionSprites = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
         GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
         ImageDeFond = GestionnaireDeTextures.Find(NomImage);
      }


      public override void Draw(GameTime gameTime)
      {
         GestionSprites.Draw(ImageDeFond, ZoneAffichage, Color.White);
         base.Draw(gameTime);
      }
    }
}
