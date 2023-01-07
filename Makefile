modname = Crusader
gamepath = /mnt/c/Program\ Files\ \(x86\)/Steam/steamapps/common/Outward/Outward_Defed
pluginpath = BepInEx/plugins
sideloaderpath = $(pluginpath)/$(modname)/SideLoader
unityassets = resources/unity/Particles/Assets/AssetBundles

dependencies = CustomWeaponBehaviour EffectSourceConditions HolyDamageManager SynchronizedWorldObjects TinyHelper ImpendingDoom

assemble:
	# common for all mods
	mkdir -p public/$(pluginpath)/$(modname)
	cp -u bin/$(modname).dll public/$(pluginpath)/$(modname)/
	for dependency in $(dependencies) ; do \
		cp -u ../$${dependency}/bin/$${dependency}.dll public/$(pluginpath)/$(modname)/ ; \
	done
	
	# crusader specific
	mkdir -p public/$(sideloaderpath)/Items
	mkdir -p public/$(sideloaderpath)/Texture2D
	mkdir -p public/$(sideloaderpath)/AssetBundles
	
	mkdir -p public/$(sideloaderpath)/Items/Aura\ of\ Smiting/Textures
	cp -u resources/icons/aura_of_smiting.png                  public/$(sideloaderpath)/Items/Aura\ of\ Smiting/Textures/icon.png
	cp -u resources/icons/aura_of_smiting_small.png            public/$(sideloaderpath)/Items/Aura\ of\ Smiting/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Blessed\ Determination/Textures
	cp -u resources/icons/blessed_determination.png            public/$(sideloaderpath)/Items/Blessed\ Determination/Textures/icon.png
	cp -u resources/icons/blessed_determination_small.png      public/$(sideloaderpath)/Items/Blessed\ Determination/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Blessing\ of\ Protection/Textures
	cp -u resources/icons/blessing_of_protection.png           public/$(sideloaderpath)/Items/Blessing\ of\ Protection/Textures/icon.png
	cp -u resources/icons/blessing_of_protection_small.png     public/$(sideloaderpath)/Items/Blessing\ of\ Protection/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Channel\ Divinity/Textures
	cp -u resources/icons/blessing_of_protection.png           public/$(sideloaderpath)/Items/Channel\ Divinity/Textures/icon.png
	cp -u resources/icons/blessing_of_protection_small.png     public/$(sideloaderpath)/Items/Channel\ Divinity/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Cure\ Wounds/Textures
	cp -u resources/icons/cure_wounds.png                      public/$(sideloaderpath)/Items/Cure\ Wounds/Textures/icon.png
	cp -u resources/icons/cure_wounds_small.png                public/$(sideloaderpath)/Items/Cure\ Wounds/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Divine\ Smite/Textures
	cp -u resources/icons/retributive_smite.png                public/$(sideloaderpath)/Items/Divine\ Smite/Textures/icon.png
	cp -u resources/icons/retributive_smite_small.png          public/$(sideloaderpath)/Items/Divine\ Smite/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Holy\ Shock/Textures
	cp -u resources/icons/holy_shock.png                       public/$(sideloaderpath)/Items/Holy\ Shock/Textures/icon.png
	cp -u resources/icons/holy_shock_small.png                 public/$(sideloaderpath)/Items/Holy\ Shock/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Judgement/Textures
	cp -u resources/icons/judgement.png                        public/$(sideloaderpath)/Items/Judgement/Textures/icon.png
	cp -u resources/icons/judgement_small.png                  public/$(sideloaderpath)/Items/Judgement/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Consecration/Textures
	cp -u resources/icons/consecration.png                     public/$(sideloaderpath)/Items/Consecration/Textures/icon.png
	cp -u resources/icons/consecration_small.png                     public/$(sideloaderpath)/Items/Consecration/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Meditate/Textures
	cp -u resources/icons/meditate.png                         public/$(sideloaderpath)/Items/Meditate/Textures/icon.png
	cp -u resources/icons/meditate_small.png                   public/$(sideloaderpath)/Items/Meditate/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Rebuking\ Smite/Textures
	cp -u resources/icons/rebuking_smite.png                   public/$(sideloaderpath)/Items/Rebuking\ Smite/Textures/icon.png
	cp -u resources/icons/rebuking_smite_small.png             public/$(sideloaderpath)/Items/Rebuking\ Smite/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Restoration/Textures
	cp -u resources/icons/restoration.png                      public/$(sideloaderpath)/Items/Restoration/Textures/icon.png
	cp -u resources/icons/restoration_small.png                public/$(sideloaderpath)/Items/Restoration/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Retributive\ Smite/Textures
	cp -u resources/icons/retributive_smite.png                public/$(sideloaderpath)/Items/Retributive\ Smite/Textures/icon.png
	cp -u resources/icons/retributive_smite_small.png          public/$(sideloaderpath)/Items/Retributive\ Smite/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Shield\ of\ Faith/Textures
	cp -u resources/icons/shield_of_faith.png                  public/$(sideloaderpath)/Items/Shield\ of\ Faith/Textures/icon.png
	cp -u resources/icons/shield_of_faith_small.png            public/$(sideloaderpath)/Items/Shield\ of\ Faith/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Thunderous\ Smite/Textures
	cp -u resources/icons/thunderous_smite.png                 public/$(sideloaderpath)/Items/Thunderous\ Smite/Textures/icon.png
	cp -u resources/icons/thunderous_smite_small.png           public/$(sideloaderpath)/Items/Thunderous\ Smite/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Wrathful\ Smite/Textures
	cp -u resources/icons/wrathful_smite.png                  public/$(sideloaderpath)/Items/Wrathful\ Smite/Textures/icon.png
	cp -u resources/icons/wrathful_smite_small.png            public/$(sideloaderpath)/Items/Wrathful\ Smite/Textures/skillicon.png
	
	cp -u resources/textures/burstOfDivinityIcon.png           public/$(sideloaderpath)/Texture2D/
	cp -u resources/textures/healingSurgeIcon.png              public/$(sideloaderpath)/Texture2D/
	cp -u ../ImpendingDoom/resources/textures/impendingDoomIcon.png             public/$(sideloaderpath)/Texture2D/
	cp -u resources/textures/impendingDoomImbueIcon.png        public/$(sideloaderpath)/Texture2D/
	cp -u resources/textures/radiatingIcon.png                 public/$(sideloaderpath)/Texture2D/
	cp -u resources/textures/surgeOfDivinityIcon.png           public/$(sideloaderpath)/Texture2D/
	
	cp -u $(unityassets)/holy_shock                            public/$(sideloaderpath)/AssetBundles/
	cp -u $(unityassets)/zealous_blade                         public/$(sideloaderpath)/AssetBundles/
	cp -u $(unityassets)/consecrated_ground                    public/$(sideloaderpath)/AssetBundles/
	# cp -u $(unityassets)/divinesmite                             public/$(sideloaderpath)/AssetBundles/

publish:
	make clean
	make assemble
	rar a $(modname).rar -ep1 public/*
	
	cp -r public/BepInEx thunderstore
	mv thunderstore/plugins/$(modname)/* thunderstore/plugins
	rmdir thunderstore/plugins/$(modname)
	
	(cd ../Descriptions && python3 $(modname).py)
	
	cp -u resources/manifest.json thunderstore/
	cp -u README.md thunderstore/
	cp -u resources/icon.png thunderstore/
	(cd thunderstore && zip -r $(modname)_thunderstore.zip *)
	cp -u ../tcli/thunderstore.toml thunderstore
	(cd thunderstore && tcli publish --file $(modname)_thunderstore.zip) || true
	mv thunderstore/$(modname)_thunderstore.zip .

install:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -u -r public/* $(gamepath)
clean:
	rm -f -r public
	rm -f -r thunderstore
	rm -f $(modname).rar
	rm -f $(modname)_thunderstore.zip
	rm -f resources/manifest.json
	rm -f README.md
info:
	echo Modname: $(modname)
play:
	(make install && cd .. && make play)
edit:
	nvim ../Descriptions/$(modname).py
readme:
	(cd ../Descriptions/ && python3 $(modname).py)
