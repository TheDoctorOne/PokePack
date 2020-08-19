package me.mahmutkocas.amialive;

import net.minecraft.init.Blocks;
import net.minecraftforge.common.MinecraftForge;
import net.minecraftforge.fml.common.Mod;
import net.minecraftforge.fml.common.Mod.EventHandler;
import net.minecraftforge.fml.common.event.FMLInitializationEvent;
import net.minecraftforge.fml.common.event.FMLPreInitializationEvent;
import net.minecraftforge.fml.relauncher.Side;
import org.apache.logging.log4j.Logger;

import java.net.DatagramSocket;
import java.net.SocketException;

@Mod(modid = AmIAlive.MODID, name = AmIAlive.NAME, version = AmIAlive.VERSION)
public class AmIAlive
{
    public static final String MODID = "amialive";
    public static final String NAME = "Am I Alive Mod";
    public static final String VERSION = "1.0";
    protected static String USER_LOG = "%appdata%";
    private Side side;

    @EventHandler
    public void preInit(FMLPreInitializationEvent event)
    {
        side = event.getSide();
        if(side == Side.SERVER) {
            USER_LOG = (System.getProperty("user.home") + "/AppData/Roaming/connecteduser.dat").replaceAll("//","/");
            USER_LOG = USER_LOG.replaceAll("\\\\", "/");
        }
    }

    @EventHandler
    public void init(FMLInitializationEvent event)
    {
        if(event.getSide().equals(Side.CLIENT))
        {
            try {
                AliveChecker.socket = new DatagramSocket(References.localPort);
                AliveChecker.socket.setSoTimeout(10);
            } catch (SocketException e) {
                e.printStackTrace();
            }
            MinecraftForge.EVENT_BUS.register(AliveChecker.class);
        }
        else if(event.getSide().equals(Side.SERVER)) {
            System.out.println("Registered.");
            MinecraftForge.EVENT_BUS.register(LoginChecker.class);
        }
    }
}
