package me.mahmutkocas.amialive;

import net.minecraft.entity.player.EntityPlayerMP;
import net.minecraft.util.text.TextComponentString;
import net.minecraftforge.event.entity.EntityJoinWorldEvent;
import net.minecraftforge.fml.common.eventhandler.SubscribeEvent;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

public class LoginChecker {
    @SubscribeEvent
    public static void checkAccessPoint(EntityJoinWorldEvent event) {
        if(event.getEntity() instanceof EntityPlayerMP) {
            EntityPlayerMP playerMP = (EntityPlayerMP) event.getEntity();
            System.out.println("User entered.");
            String path = AmIAlive.USER_LOG;

            System.out.println("Path: " + path);
            try {
                FileReader fr = new FileReader(path);
                BufferedReader br = new BufferedReader(fr);
                String tmp = "";
                while ((tmp = br.readLine()) != null)
                {
                    System.out.println(tmp);
                    if(tmp.trim().equals(""))
                        continue;
                    String[] data = tmp.split(";"); // [0] Username , [1] IP Address
                    if(playerMP.getName().equals(data[0])) {
                        if (playerMP.getPlayerIP().equals(data[1]))
                            System.out.println("Granted connection to " + data[0]);
                        else {
                            System.out.println("Wrong IP Address Connection IP:" + data[1] + "- Username:" + data[0]);
                            playerMP.connection.disconnect(new TextComponentString("Start your game from launcher."));
                        }
                    }

                }

            } catch (IOException ignored) {}

        }

    }
}
