modname = Crusader
gamepath = /mnt/c/Program\ Files\ \(x86\)/Steam/steamapps/common/Outward
pluginpath = BepInEx/plugins

dependencies = CustomWeaponBehaviour EffectSourceConditions HolyDamageManager SynchronizedWorldObjects TinyHelper

publish:
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	mkdir -p public/$(pluginpath)/$(modname)
	cp bin/$(modname).dll public/$(pluginpath)/$(modname)/
	for dependency in $(dependencies) ; do \
		cp ../$${dependency}/bin/$${dependency}.dll public/$(pluginpath)/$(modname)/ ; \
	done
	rm -f $(modname).rar
	rar a $(modname).rar -ep1 public/*
install:
	make publish
	cp $(modname).rar $(gamepath)
	yes | unrar x $(gamepath)/$(modname).rar $(gamepath)
	rm $(gamepath)/$(modname).rar
clean:
	rm -f -r public
	rm -f $(modname).rar
	rm -f -r bin
info:
	echo Modname: $(modname)
