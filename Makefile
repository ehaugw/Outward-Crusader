include Makefile.helpers
modname = Crusader
unityassetbundles = resources/unity/Particles/Assets/AssetBundles
dependencies = BaseDamageModifiers CustomGrip EffectSourceConditions HolyDamageManager ImpendingDoom SynchronizedWorldObjects TinyHelper

assemble:
	# common for all mods
	rm -f -r public
	@make dllsinto TARGET=$(modname)
	
	@make basefolders
	
	@make skill NAME="Aura\ of\ Smiting" FILENAME="aura_of_smiting"
	@make skill NAME="Blessed\ Determination" FILENAME="blessed_determination"
	@make skill NAME="Blessing\ of\ Protection" FILENAME="blessing_of_protection"
	@make skill NAME="Channel\ Divinity" FILENAME="blessing_of_protection"
	@make skill NAME="Cure\ Wounds" FILENAME="cure_wounds"
	@make skill NAME="Divine\ Smite" FILENAME="retributive_smite"
	@make skill NAME="Holy\ Shock" FILENAME="holy_shock"
	@make skill NAME="Judgement" FILENAME="judgement"
	@make skill NAME="Consecration" FILENAME="consecration"
	@make skill NAME="Meditate" FILENAME="meditate"
	@make skill NAME="Rebuking\ Smite" FILENAME="rebuking_smite"
	@make skill NAME="Restoration" FILENAME="restoration"
	@make skill NAME="Retributive\ Smite" FILENAME="retributive_smite"
	@make skill NAME="Shield\ of\ Faith" FILENAME="shield_of_faith"
	@make skill NAME="Thunderous\ Smite" FILENAME="thunderous_smite"
	@make skill NAME="Wrathful\ Smite" FILENAME="wrathful_smite"
	
	@make texture FILENAME="ancestralMemoryIcon"
	@make texture FILENAME="surgeOfMemoriesIcon"
	@make texture FILENAME="burstOfDivinityIcon"
	@make texture FILENAME="healingSurgeIcon"
	@make texture FILENAME="impendingDoomIcon" PREPATH="../ImpendingDoom/"
	@make texture FILENAME="impendingDoomImbueIcon"
	@make texture FILENAME="radiatingIcon"
	@make texture FILENAME="surgeOfDivinityIcon"
	
	@make assetbundle FILENAME="holy_shock"
	@make assetbundle FILENAME="zealous_blade"
	@make assetbundle FILENAME="consecrated_ground"
	# @make assetbundle FILENAME="divinesmite"

forceinstall:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -u -r public/* $(gamepath)

play:
	(make install && cd .. && make play)
