modname = Crusader
gamepath = /mnt/c/Program\ Files\ \(x86\)/Steam/steamapps/common/Outward
pluginpath = BepInEx/plugins
sideloaderpath = $(pluginpath)/$(modname)/SideLoader

dependencies = CustomWeaponBehaviour EffectSourceConditions HolyDamageManager SynchronizedWorldObjects TinyHelper

assemble:
	# common for all mods
	rm -f -r public
	mkdir -p public/$(pluginpath)/$(modname)
	cp bin/$(modname).dll public/$(pluginpath)/$(modname)/
	for dependency in $(dependencies) ; do \
		cp ../$${dependency}/bin/$${dependency}.dll public/$(pluginpath)/$(modname)/ ; \
	done
	
	# crusader specific
	mkdir -p public/$(sideloaderpath)/Items
	mkdir -p public/$(sideloaderpath)/Texture2D
	mkdir -p public/$(sideloaderpath)/AssetBundles
	
	mkdir -p public/$(sideloaderpath)/Items/Aura\ of\ Smiting/Textures
	cp resources/icons/aura_of_smiting.png                  public/$(sideloaderpath)/Items/Aura\ of\ Smiting/Textures/icon.png
	cp resources/icons/aura_of_smiting_small.png            public/$(sideloaderpath)/Items/Aura\ of\ Smiting/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Blessed\ Determination/Textures
	cp resources/icons/blessed_determination.png            public/$(sideloaderpath)/Items/Blessed\ Determination/Textures/icon.png
	cp resources/icons/blessed_determination_small.png      public/$(sideloaderpath)/Items/Blessed\ Determination/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Blessing\ of\ Protection/Textures
	cp resources/icons/blessing_of_protection.png           public/$(sideloaderpath)/Items/Blessing\ of\ Protection/Textures/icon.png
	cp resources/icons/blessing_of_protection_small.png     public/$(sideloaderpath)/Items/Blessing\ of\ Protection/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Channel\ Divinity/Textures
	cp resources/icons/blessing_of_protection.png           public/$(sideloaderpath)/Items/Channel\ Divinity/Textures/icon.png
	cp resources/icons/blessing_of_protection_small.png     public/$(sideloaderpath)/Items/Channel\ Divinity/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Cure\ Wounds/Textures
	cp resources/icons/cure_wounds.png                      public/$(sideloaderpath)/Items/Cure\ Wounds/Textures/icon.png
	cp resources/icons/cure_wounds_small.png                public/$(sideloaderpath)/Items/Cure\ Wounds/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Divine\ Smite/Textures
	cp resources/icons/retributive_smite.png                public/$(sideloaderpath)/Items/Divine\ Smite/Textures/icon.png
	cp resources/icons/retributive_smite_small.png          public/$(sideloaderpath)/Items/Divine\ Smite/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Holy\ Shock/Textures
	cp resources/icons/holy_shock.png                       public/$(sideloaderpath)/Items/Holy\ Shock/Textures/icon.png
	cp resources/icons/holy_shock_small.png                 public/$(sideloaderpath)/Items/Holy\ Shock/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Judgement/Textures
	cp resources/icons/judgement.png                        public/$(sideloaderpath)/Items/Judgement/Textures/icon.png
	cp resources/icons/judgement_small.png                  public/$(sideloaderpath)/Items/Judgement/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Meditate/Textures
	cp resources/icons/meditate.png                         public/$(sideloaderpath)/Items/Meditate/Textures/icon.png
	cp resources/icons/meditate_small.png                   public/$(sideloaderpath)/Items/Meditate/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Rebuking\ Smite/Textures
	cp resources/icons/rebuking_smite.png                   public/$(sideloaderpath)/Items/Rebuking\ Smite/Textures/icon.png
	cp resources/icons/rebuking_smite_small.png             public/$(sideloaderpath)/Items/Rebuking\ Smite/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Restoration/Textures
	cp resources/icons/restoration.png                      public/$(sideloaderpath)/Items/Restoration/Textures/icon.png
	cp resources/icons/restoration_small.png                public/$(sideloaderpath)/Items/Restoration/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Retributive\ Smite/Textures
	cp resources/icons/retributive_smite.png                public/$(sideloaderpath)/Items/Retributive\ Smite/Textures/icon.png
	cp resources/icons/retributive_smite_small.png          public/$(sideloaderpath)/Items/Retributive\ Smite/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Shield\ of\ Faith/Textures
	cp resources/icons/shield_of_faith.png                  public/$(sideloaderpath)/Items/Shield\ of\ Faith/Textures/icon.png
	cp resources/icons/shield_of_faith_small.png            public/$(sideloaderpath)/Items/Shield\ of\ Faith/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Thunderous\ Smite/Textures
	cp resources/icons/thunderous_smite.png                 public/$(sideloaderpath)/Items/Thunderous\ Smite/Textures/icon.png
	cp resources/icons/thunderous_smite_small.png           public/$(sideloaderpath)/Items/Thunderous\ Smite/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Wrathful\ Smite/Textures
	cp resources/icons/wrathful_smite.png                  public/$(sideloaderpath)/Items/Wrathful\ Smite/Textures/icon.png
	cp resources/icons/wrathful_smite_small.png            public/$(sideloaderpath)/Items/Wrathful\ Smite/Textures/skillicon.png
	
	cp resources/textures/burstOfDivinityIcon.png           public/$(sideloaderpath)/Texture2D/
	cp resources/textures/healingSurgeIcon.png              public/$(sideloaderpath)/Texture2D/
	cp resources/textures/impendingDoomIcon.png             public/$(sideloaderpath)/Texture2D/
	cp resources/textures/impendingDoomImbueIcon.png        public/$(sideloaderpath)/Texture2D/
	cp resources/textures/radiatingIcon.png                 public/$(sideloaderpath)/Texture2D/
	cp resources/textures/surgeOfDivinityIcon.png           public/$(sideloaderpath)/Texture2D/
	
	cp resources/assetbundles/divinesmite                   public/$(sideloaderpath)/AssetBundles/
	
publish:
	make assemble
	rm -f $(modname).rar
	rar a $(modname).rar -ep1 public/*

install:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -r public/* $(gamepath)
clean:
	rm -f -r public
	rm -f $(modname).rar
	rm -f -r bin
info:
	echo Modname: $(modname)
