using Mcv.Plugin.A17Live.InternalMessage;
using NUnit.Framework;

namespace _17LiveSitePluginTests;


[TestFixture]
public class MessageParserTests
{
    [Test]
    public void ParseUnknownMessageTest()
    {
        var m = MessageParser.Parse("a");
        Assert.IsTrue(m is UnknownMessage);
    }
    [Test]
    public void ParseUnknownJsonMessageTest()
    {
        var m = MessageParser.Parse("{\"a\":\"b\"}");
        Assert.IsTrue(m is UnknownMessage);
    }
    [Test]
    public void ParseHeartbeatTest()
    {
        var m = MessageParser.Parse("{\"action\":0}");
        if (m is not Heartbeat)
        {
            Assert.Fail();
            return;
        }
    }
    [Test]
    public void ParseConnectedTest()
    {
        var m = MessageParser.Parse("{\"action\":4,\"connectionId\":\"8UQqAkw9MV\",\"connectionKey\":\"11dQy91hgB4y80!8UQqAkw9MVLpFPIk-de5ff11dQy91hgB4y80\",\"connectionSerial\":-1,\"connectionDetails\":{\"clientId\":\"*\",\"connectionKey\":\"11dQy91hgB4y80!8UQqAkw9MVLpFPIk-de5ff11dQy91hgB4y80\",\"maxMessageSize\":131072,\"maxInboundRate\":120,\"maxOutboundRate\":120,\"maxFrameSize\":262144,\"serverId\":\"frontend.f45d.1.ap-northeast-1-A.i-0cea7ba80bdd8958f.11dQy91hgB4y80\",\"connectionStateTtl\":120000,\"maxIdleInterval\":15000}}");
        if (m is not Connected)
        {
            Assert.Fail();
            return;
        }
    }
    [Test]
    public void ParseAttachedTest()
    {
        var m = MessageParser.Parse("{\"action\":11,\"flags\":786432,\"channel\":\"376902\",\"channelSerial\":\"11d43y0UwB4xuD94907466:26197\"}");
        if (m is not Attached)
        {
            Assert.Fail();
            return;
        }
    }
    [Test]
    public void ParseMessageCommentTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"QgubyvCqAI:0\",\"connectionSerial\":4,\"channel\":\"1266543\",\"channelSerial\":\"11dUZxPMwB4vcq73645518:376337\",\"timestamp\":1637813594618,\"messages\":[{\"data\":\"H4sIAAAAAAAA/9RXT48bSRX/LrVXd+L//24zHg94lSwjO1kOBKHXVc/tirurequqPWOiSLZnESsgCiCh3UCkEWhRgBW5gEQOCL7LtiYRJ+YjoOput7s9BnJdn/r9fvWqXr2/5SfELEMk/UaFLFBpLgXpi8j3K0Tg+YeSizF+EqE2W/ScmxlTcIgCSjE0BwiFj5EeIh5bSMpgK/sICywCc07nxe/j5WS+xCVuMRTg+njsg7tFGNf7kEoPHV6EXOWaISjDKQ9B7BHaKIQA1UAKgdScAve3FJVBgMLc1x7pPyEBqE8iRPuZurBaIaHkwpB+9WmFuKAUeGWayiihK4TrkZhywQ2S/hR8jTvdChEQIOkT8tRaA0GYmxZNp/ziIwjwAV7kTuT6hCuzzPfJ5IcaVY4p9JLIkg/PSGVnj0bBHnB7WK3d6HRrjVav2Wp3bSAW6JN+vV4hni/VUklg9yVLtXJkJKYydzKIeZJFsAADyl774fhedotQ4ZRfHANL/bEj0ps+ISa5D3n37Efvvvr99ad/un7zsx8sZTTn1lq8MAPpS0X65IPT00ZjMDyuZviE/9Aa36wQPQMmz7frLA1eQeJ6kiwgfaMifFohAWfMxwMmKXkLnhkT9u/epUzcqXUgDO9QebfW7rR6CK7TaXSaTrPboE6XQs9hUGu7UwrtJq3fCYVntwRjUDAQ9H027rIGq0IbnKrbcp1mfdpzut1aw2mxVrdVp616Y9rZbpwlZMGD8ern169//fblVbz6xSNxc/XLlzdXz3767tUXb3/z5vr1l/H6dbz5XXz5F4taNl6/itcv4tXmkYg3f4gv/x5ffhWvVvHmb/HmlV26WsWXv4o3f4wv/2xZS722yOVn8frVI3H9z5dvn/813nwWb57F68/j9fN4/ZN//faLeP1pIv4jXr+4ufry85urFz/+evXs6xebf795fjuo6e//BPWDdrua/MrRTdSHte7gUJg53TU0I8Mx92bmQNBdoHNPyUiwfNdWr96tN+onpEJcqRiqUiVnqhXCA/Dwu5yZWVrXVvw22mOyik8iNJBKoBoD45FO8HTLnV4q54o2tjOkcy7uZZVYIUHyecD4tLr220JS2/KUK3RB466FCZNkzDcmU6gNyCDpPdbfWVudmKW/a52M69CHrOU9IZFGNTohfdLpubRa69UcaDU7TrPeRafV7nScDvY6U1rt0DZrkFz9o7TtHmxCIacmskOC1AadRrtzcuwMB6enTrN5euL0mq2uU2tXj5qN9qDZ6w3uPA49UuqiXH88Oiu06G9FoBgHkUO3wh0qySJqL1RNBE9BkKaKzYA8/8Jp2ldzYJvlIypFDrrFXjgtCpQrmjW8fDWoYDlO+nmtQmagz7an5+af5cbl0CSbm/tz6H/OpSC7bzV3wDHzcjsyaBCYHHqvaVSeOJr0v/d9O0ojV1PFXVRDYVAlU3z7QABqCnIhnQqol4WsOPU8Pi0qer50wb8X0fnyGLwC4fMFpg46tqZS0LfV7L4PQwYGixQEuLcPL5wfyjmOSrLvF+UAGYf7qHXyEimediSEjATF/EGzb+iZkoE0XIoCaxRfcCh5jg3FAn0Z4nCBwpReBHvcULDDmuPkcVJUXUhTupaSdP4xx3NUurBFsDxSwfI7C1QLjucFAm18jwQPYM9+hmBmwwsMwoRABiVvGR7gZCloQcOWwkgsuNnfaqpQz2yG7CdTGGwr4b84Y4zpUCh6I5BCm7KGkeUkyZ6bBWT7Uj3SmmsDpTjiIpk6wV54F5yhHMygFHJwpRrjOShmiz7Kn5sMloMZiF3mZCsp8gWmCkX7uLb/HsYYcMFyZ8yP6J7jwvmEyt1zG7ZhKqXxNvdGR2dFnAH3lw9Az0uu5ZoLb2KgVKxKRuEAfP/W+TlzHwMX1cSAiYpZRZMr2z8hOfj0PwAAAP//AQAA//+5jrsUJQ0AAA==\"}]}");
        if (m is not Comment message)
        {
            Assert.Fail();
            return;
        }
        Assert.AreEqual("QgubyvCqAI:0", message.Id);
        Assert.AreEqual(1637813594568, message.SendTime);
        Assert.AreEqual("【定期】\n💜🍋秘書室のアン🍋💜です。\nエール　シェア　フォロー　ギフトで\n応援よろしくお願いします🤗🙇‍♂️", message.Text);
    }
    [Test]
    public void ParseNewGiftTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"FNAvzOTy9n:0\",\"connectionSerial\":1,\"channel\":\"15249444\",\"channelSerial\":\"11d-haoAgB4xu692134235:29329\",\"timestamp\":1637847776235,\"messages\":[{\"data\":\"H4sIAAAAAAAA/4xWzY7bRhJ+lz6LY5JDSZZukmbW0MKzO5A8viwMo9hdpHrY7Ka7m9RwjTmMYRgBEiCXXHIycvA75HmiB4gfISApckh5EvjG+qqrq+qrn+Z7YssMydw7H5ECteFKkrnMhRgRift/Ky43+C5HY1t0z+2OaXhKBZRiZp9QaLxF+pTitoKUSltZIBTYBxJOk/73stwmJZbYYighFLgUELYI4+YU0o3Ty7uM684yA2055RnIE4WxGiFFvVJSIrX/Ai5aFVVpitJembg7nIeGah6ivpQWdU+jEWj/JOMmE1DemMGpOAfNOMi1jFSH8agxfN83qsTcoF5fkDk5B8AJzJ474wl77gTehDnhJEDHpwyiMMApC4B0Pv8DKZI5OTx8PDz8dnj45fDh4x+/fvnz95+/fv7ph6+fv3w4fPh0ePh0ePiRjEjGqc0rNohPZ34UBIHjRlHoBN7Ud8LAP3c8H3yMvCicRsHZbRaTqm4FCjIfuyPCzev1NZlHIAxW0otjhh1Ed0gTLl82JpMRybRiOa0ydGsh1pDW3yGwGG82L8mcVIFFd8sBYFW24fHOrqmSHRjGKyWUboSoL1CuqcDhFaDTcgMyqd3twFy33rvwr7vgOmh7bJAedMG1LU/lpmpHLD3m63YELFncxLGzNjPzZ8/AGLTG8aaQZWeQQAr8/8jOJNpnUz8Mxu40csYRBk7A2NgBd8yccOr6nj8Nx8EYzzJZleJ4+yq13327546nXng+cyIvdJ3ARXBmIZ0404kX+nQW0tlkdrw9FkqXWgG7UgzrbDrksYXvmx6uO9X3PO/tbfbWqqwdLGNBvzUV+wVY0G3bc7NFyRZCdJxlikvbOOGRfaUSlGTu+ZOJN/OezyYjgneNE9I6NGT+v39w+eY4XGihOvmeWNAx2hffE6zIaVIu4XHyhbK9vRCqakCpyquIvREpQHB2kWuwXMkrQ+bnruuOCEie1tAGjco1xTb+TtH2yX2Vf/K4rpTo1lAIWkOMlUOzU/tlK9a03R+n8SZrj+9AsgsN+63lNMGO7zqhCm8Bi3fN5B/1UKq8SzGud0hd3Tf3ddVDEC+PnPQWmuAFNgOyrJqCgukvwcas6pSbjIHFvgpSPLmH95ZixcV6IAvRl1NkHK7QmJqJvreFlCqXFE8292Og11qlqilTp7WaFxwG65xdygKFyvCyQGn7vk91l5I9bbnBmKvBri+UHaSlFU1ec9yjNr0r0nKh0/K/BeqC476nwOrRWbSt039tEOzu8g7TrFYggwFblqe4LSXtWVSrcC0Lbk+vijSaXbXLTl+4LG034d+QscHmweyzkSpp7NDCqmGTpKDf5dhvhnYYF8ZwY2FQR6wYX33zMBecoVrtYFByCJXe4B40q5Z+3k0Ig3K1AxnjyUmKvMDGoB8fN6ae4JRL1pGRLOgJcVmypepxnroJH7Rx23vrxXUfZ8BF+QpMMqCWGy7jrYXBH4RWebYCIb7x32muMA1Rby3YvN9VtE65+tvqwPu/AAAA//8BAAD//3uJdVcPCgAA\"}]}");
        if (m is not NewGift obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void ParseLiveTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"kPVGQVghEH:0\",\"connectionSerial\":5,\"channel\":\"15249444\",\"channelSerial\":\"11d-haoAgB4xu692134235:29333\",\"timestamp\":1637847780618,\"messages\":[{\"data\":\"H4sIAAAAAAAA/3RUzXLiPBB8F585fL+1WW5AsVvZ2lRSkOQ+SA1MLI0USRZxbeXdtwwxyA57s7vV+unpmV9Vaj2q6b83kyojRHZSTaUxZlIJDj8cywqvDWLq0QOnvQ50jSKl4NMVIuAF6hrx0kHO2f7fgDJKoGZVl9/zdl23aNFjENoYzA1tekRzHEPhdOjyzXM4Kz2FxIo9yYiIKYAswsKJQKVvxKanlLMWku7i7ry42UQVeIOwlIRQMAGkypWaozfUPsXBql1DQTPJrWzdGeNtKdwZtyHzs1F1O6ddQRjOWB8vOw+OtKL4Wdbt++Q1JZQUWYz24eP5fRj+nlS2SaimWzIRpxutSOp7QSl6Zhw6nxpJ1fTLf3919cswj+67Kx1zmlN7yciekdHZ+EymQSf8/+vNP++TyrsapQ/eGVP+W2imO8RIOwxfORNxjSiMinMx6CE46xI7KdgUODMNKqaXkmGcxzJDUnn2mFuKvq5cYcduUM7s0uBZwan65FwstrDtLNj2PiNkxqEg0OVqJmxpdH8NSvvlG6w/EtA0cCuxxboVVSgo2PZWMqfxVtuAuO+SOQ6xt+uPbviDGSuceqJ0wzqJaahIbhhOS+G1QRnCvutmMXJMNKgjOscXn3ovs4Zb7GlQctq4sMKBgu4C2/izW9Qu9iSX5HysVOCMk6C8H8duEq5gWfTZjHqmRsb5eq3cZXRQX6ZBjPvs3c4eSlwTm/aRYj2wliPLbp1oMCSCa/yCjPl0/pm5g90grBOlpkyVOj65G6hn8P03AAAA//8BAAD//7oo8uTyBQAA\"}]}");
        if (m is not Live obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void ParseSubscriberEnterLiveTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"685VTk1Qc9:0\",\"connectionSerial\":10,\"channel\":\"15249444\",\"channelSerial\":\"11d-haoAgB4xu692134235:29338\",\"timestamp\":1637847787574,\"messages\":[{\"data\":\"H4sIAAAAAAAA/3RU3XLyNhB9F13jTvn/zB1QOkPnS5qBpvdraTEb68eRZBNPJhfp9CU6fYg+FC/SMWAjE7476xzvrvbsWb0zX+XIZoNpj5VoHRnNZrqQssc0Hn4zpDf4WqDzDXogvxcW7lHAOeb+DmHxBfk94qWGjFHNWSKUGAIZ8Sz8XlTbrMIKGww1JBIXEpIGEeRuIXsuunrLybaROVhPnHLQN4TzFkGhXRqtkftfgWRDcaMUav/g0vbnInHcUoJ2pT3aE/Ne3yGXUD2CQjZjx8//jp//Hv/6+/j5D+uxnLgv6nIsHg5iHA/H0bcYRDSajAdRPIl30bA/5LGI+8loIn56yVPWY6BJgT8NZ9pj6juWKNns5x7DErV/NJ52wa0Kh3b9C5uxJE6gD1MR4XQ0ika7yTiC/rAfDae4m/a/jQejpM9q2U/pBsOPWizgYYeXXp7dpbszmhZgBYFe651pMdqFgak0CcjvBc+qBaQBIanE7UnkhTUgOLivYXXe51yAx5AChTd5KKifmwzXnbOU4VmhIHhA5yDFbrW51qbQHG+Ge73okzXK1PIHrLdUEgSARbHSJUqT46qeSlj7lltpcT9ygymZjqyl8Z22rOHZn4QHtC5Ioaq5VdXvJdqS8BAQWPty3tgnHCyC36/eUOUnAgV01PKkcFtpHkSAVdVal+RvU+0sun3tkOsSXEagtpdt+oEYGzzvVKiGMtr5boQ3XZMosK8FhmZotnbuHDkPnTmedmT5ZXdLEmiWe+iMHBJjN3gAKzagsyJv1YJquQd9dc7lT45U4jkgvB+5+iXdoCItWjGyOb8RLs+23FyfnnbLOzZuvLeeP4W4AJLVH+CyjrTkSKdbD51ltabIlyDll/ot84AqQbv14IvQVfzUcv0gt+DH/wAAAP//AQAA//+PWbG1MgYAAA==\"}]}");
        if (m is not SubscriberEnterLive obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void ParseRockViewerUpdateTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"D2M5WEiJ6W:0\",\"connectionSerial\":12,\"channel\":\"15249444\",\"channelSerial\":\"11d-haoAgB4xu692134235:29340\",\"timestamp\":1637847790249,\"messages\":[{\"data\":\"H4sIAAAAAAAA/3RUy27bMBD8F59zLFAgN9twARcNGthN72tyIm9ELpklJUco+u+FkkghFfcmzuxLs0P+WeUhYnX79cvNqocmDrK6lc65m5Xg8j2wHPDcIeUJvXA+W6VrFBmDmK8QiieYa8TTCIXgp7MD9SiBlk1bfm+GYztgwIRB6OSwcXSaEMtpCelb091LZJ0zI2lmw5FkQaSsIA/dBhGY/I3YTZQJ3kPyXWrm4O6UjPIJupMMLRgFmTLScoqOhodURTUdqWWSvTyGGePHMrFx4UTuR2faYUNNQTjucXwddqOBrKH0OW2s+xAtZZQUeSzqcNE/hhb76uxcefawTHdIiRrU3dYioRODhUgfg95r8CFzkILNyj1TpZzdSQ8XInY9JJe9l9xO7PXMAxoOlax9yNVvaTDtb8YFmooSflirH3720J5xKQiM+10Le1rMb0H5vHuBj68ELFVqZfY4DmKKDFI/7KXnvCz1qEjn0SFLM0V/fHflf8Q44M2bpRo+SMp1Rg61STzpc4fSDJP71ylxylTtEaPi2093oGeLsD1TtXI6BT3gQmoPJG0XZ7Vo2J5JPpzzHmnAPd4Syvk4jS/SAZ7FzmK0a7MQLrZHEz6uME1rqmw8eW+/vi9xS+yGX5TaSlpOLM0xU3VZNXRxS8596j8zd/An6DFT7kpXmddfHh+2Gfz7DwAA//8BAAD//yHBpwF6BQAA\"}]}");
        if (m is not RockViewerUpdate obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void ParseLiveStreamInfoChangeTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"-aQHEdn33n:0\",\"connectionSerial\":15,\"channel\":\"15249444\",\"channelSerial\":\"11d-haoAgB4xu692134235:29343\",\"timestamp\":1637847795021,\"messages\":[{\"data\":\"H4sIAAAAAAAA/3RUy27jOBD8F59z3kNutuEFvNhgA3sz9xZZkTsim0yTkiMM5t8HSiKFVDw3sapfqi7y5yaPEZv7v+42AzRxkM299M7dbQTXfwLLCa89Up7RK+eLVbpFkTGI+QaheIG5RbxMUAh+PjvQgBLo2HTl9248dyNGzBiEGoedo2ZGLKc1pB9ND2+RdcmMpJkNR5IVkbKCPHQfRGDy38RupkzwHpIfUrsE900yyg30IBlaMAoyZaTlFB2NT6mKantSyyRHeQ4Lxs9lYutCQ+7f3nTjjtqCcDzg/D7sTgNZQ+l72lT3KVrKKCnyWNXhon8MHY7V2bny7GGZHpAStai7bUVCLwYrkb4GfdTgQ+YgBZuVB6ZKOXuQAS5EHAZILnuvuYPY25kntBwqWYeQq9/SYLofjCs0FSX8uFU//jdAB8a1IDDtdyvsaTW/BeXL4Q0+vhOwVKmV2eM8iikySP14lIHzutSzIl0mh6zNFP3505V/EOOED2+WavggKdcZOdQm8aSvPUozzO7fpsQpU7VHTIrvv92BgS3C/kLVyqkJesKV1J5Iuj4uatG4v5B8Oecz0oAHfCSU83GaXqQTPItdxOi2ZiVc7M4mfF1hmtdU2Xj23nH7WOKW2I3/U+oqaTmxtOdM1WXV0Mc9Ofet/8I8wDfQc6bcl64y7788PWwL+Os3AAAA//8BAAD//xoO6rx5BQAA\"}]}");
        if (m is not LiveStreamInfoChange obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void ParseReactMsgTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"2kOti2z7--:0\",\"connectionSerial\":36,\"channel\":\"15249444\",\"channelSerial\":\"11d-haoAgB4xu692134235:29364\",\"timestamp\":1637847822997,\"messages\":[{\"data\":\"H4sIAAAAAAAA/+RWXXOrNhD9LzxbMd/YfrN93Y47SZuxb+77Ii1YASRdSZDQTv57B2NjcNKZ9rlv7Fn2rPbssuIvx7YKnZW/mDkNasOlcFaiLsuZI/DtN8nFAX/WaOwVfeP2xDR85QJKUdkvHBpfkX7leO0gKaurXSI0OAYKTovx86Y9Fi22eMVQQFripoT0ijBu7iHdJ929K66HSAXacsoViDuHsRqhQr2VQiC1vwAvry4qqwqFfTL58HKdGqp5inonLOqRRyPQ/s1B4hv39zPgdgU3WDqrMJg5UqHYf3NWjoVSSuF6fuDMnLyUutUS2JNkfciA7EUmO3YlubDOKoi9gW8xcziV4uXw6Kyck7XKrOZzMAatIV4CSj1AARXwP5E9CLRzN4iTJEgDwqJFRkIGjEAcUQLMo3GYejSOFg9K5M7MSYHluP8v5IG7pL5LPRIFcUTCIMoILFKX0GUQL8OMRYDJmfzj3D1VQvtiUHel1Qb1WRPXdWEBPhAaLEMSspiSNPGXxFtEcRwkCQV/4Qzhv0OF90IqTm3dddnJIhr6mYvES/yUhMuIkTTMQpJkNFpGYbSMg/DhVXXFjtrDzY/9s7PKoDTYWb/WoBkHMUD0hLTg4rEPCWaO0pLVtCvEPRu5hur8fJawl687WPa+mQBWqgPPT/Ymcid7vpWl1L2RjQ3KNS1xSgG6ag8ginO6E5jna/bh+M/D4QboeBnOEfSNa9ve231zLlh1qdcdBNiw/F+PRkppGjFKiZtGMQmXXkrSJPAJi1y2QJ8lCxpf5u7Cvq3sjX01n1MmHnpmKueIoRt6NCR+4rokDBFIugw84gV+FDE3i7PsSve/+q4+pl/WaEvllynua75gPBuvuLyUKZSPNS3aDeQjR8kb7Gdm08lGwXwO63hfFAOLYxdUeMfDR/mVLHA/sctybFfIODyhMZDjNNtaCFkLindL+nbQZy0rabkUI6/VvOEw2dxsJxospcJdg8KOc9/7doJ9HXnAnMuJrI20k7K0pMUPjm+ozYiiate6av9oUDcc30YO7O6XteAV3J2fIdjT7h0rdXYgg4lalld4bAUdRXTbYS8abu+pMo3m1E3I/WWmquty+AcxDtjfjWM1KimMnUZYOR2SCvTPGsfDcL0h18ZwY2HSR+wU3366gxvOUG5PMGk5pFIf8A006/ZgrQa1oN2eQNwm5/ImRd5gHzA+HzfdH9EBKy7YIEaxpnfCqeJI5e0XAq5tmozxdfb26+cxzoCX7XcwxURabrjIjxYmH6uWtdpCWX7KP3iesEpRHy3YejxV9Fxy92M1gB9/AwAA//8BAAD//4aWLYz6CQAA\"}]}");
        if (m is not ReactMsg obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void ParseLaborRewardStreamerReceiveTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"YOUoBQ6c3b:0\",\"connectionSerial\":38,\"channel\":\"15249444\",\"channelSerial\":\"11d-haoAgB4xu692134235:29366\",\"timestamp\":1637847824393,\"messages\":[{\"data\":\"H4sIAAAAAAAA/5RVzZLjNBB+F53jWTv+S3zLhFCVrR2YSna4t6V2orH1s5LsrJmaI0eegAPFgSOnLSguFC+zBexbUE7WGTszHChfrP7U3erW97UeiGs1kiydT0iDxnIlSSbrqpoQiYfXissNvqvRut564G7PDLwEAaWo3QuAwXukLwH3nUkp0a8rhAaHhpLTcvh/3W7LFlvsbSghr/C6gry3MG4vTeaUdPVec3P21GAcp1yDvACsMwgCzVJJidR9CbzqIaqEQOlu7O68uc4tNTxHs5IOzQAxCHS4k3GrK2jv7GjXrgbDOMi1LNTZxouh465SOVRvalq217AbABVvcHs87LVRwCjY525d3DvNwOEQAoEXcfggv1YlrkfrqhquBTION2gt7HCcbSGlqiXFiyY9HfTWKKEcV3KAOsMbDqPOsZVssFIaVw1KN8x9ia0ke9lzgzuuRm1tlBuVZRQtv+F4QGMHIUS7MKL9ukHTcDwMAOzudyG5gIvzMwS3X71HoY8AMhh1y3GB21bSgQcY0a5lw91lqMKg3XcMuSSTFtvPrPyPZmzwxM1hN4SS1o09nBqTRIB5V+OQDD37F9Zy62B0j9h1fPlMAw1nqJZ7GF055Mps8ACGbUCWtT53C9rlHuQTcz7vpMgbPDkcwzyQBqoaSRb4E1LbvoyH0/8XJCO+78MMpuDRcB55EUuol6fTuRfM4iQJ05TCdEbOuvsKBJKMOKiUkn4wDcmEaE5d3QmfFDGNpoWPXpBOcy+ax8zLoyLy0oLG8ziK50kYXd3rHZkQeQr096+//fPhh08/fvjr5z8+fff9xz9/+vj7L6SbYA1WJIvCCVEa5fGko6TmSEySkde35Kgb03bq7cvTiktHsjAJzrFmE8KpknebNyQje+e0zV69AmvRWS9IQesrKEEA/xbZlUT3yg+TNA3z0GPxrPAiBsyDJKYesIAmUR7QJJ5dadkVkwPb4fr/BA/9OZ36NPDiMIm9KIwLD2a579F5mMyjgsWA6TH446C4G8WQZP6EKNnNgidGPnb96O787fER8oK0+x4nRHDbPUUbFFyyswrKBb1QjC63VD3Nbuj1OZpf/dBZL26Hdga8at+CLUea4pbL3dbBaEobVeslVNWz/GfkBkWOZuvA1cNxQo9c7160s/HxXwAAAP//AQAA///uOTSIcwcAAA==\"}]}");
        if (m is not LaborRewardStreamerReceive obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void ParseNewLuckyBagTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"RpM0iS80K6:0\",\"connectionSerial\":19,\"channel\":\"15249444\",\"channelSerial\":\"11d-haoAgB4xu692134235:33647\",\"timestamp\":1637853097908,\"messages\":[{\"data\":\"H4sIAAAAAAAA/3xWS3LbOBO+C9aiTUmURGknyUpKf8X/uKQ4m6mpVBNoUjBBgAFAyhyXV7nG3GKOlItMkRRpUHGyY3+NfqAfH/hCbJUjWU0nI1KiNlxJspKFECMi8fw/xeUBvxVobIeeuT0xDe+pgFLM7TsKjU9I31M81ZBSWScLhBJdIOU0db831TGtsMIOQwmRwI2AqEMYN9eQboPunnOue8sctOWU5yCvFMZqhAz1VkmJ1H4ALjoVVVmG0t6bpD9cRIZqHqHeSYva0WgE6p5k3OQCqkczOJUUoBkHuZex6jEet4YvrlEtFgb1/o6sSBTQxWQ8W3iLOAAvoLPQgymCF8/RX06DCCMGpI/5f8iQrMiP7//8+P7veB4EZERyTm1RX5rMlvNwupiG3mQ+nniBH4ZeGO4W3ofdfBMsJn6w9Kc3T3lC6vaUKMhq7I8IN1/2D2QVgzBYSx8vF+khekKacvmpNQlGJNeKFbS+iN8IiYas+Y6AJfh4+ERWpE4sft4MAKvyA09Odk+V7MEo2SqhdCvErkC5pgKHLkBn1QFk2oQ7gXnoovfpP/TJ9dDxMgcOdMe1ra7ltjkXLLvc1+8LsGFJm8fJ2tysbm/BGLTGGy8gz28ghQz438huJNpbRhmbUTrxogAXXhDHvhfhYu6NYxpEOKP+bBzd5LJuxcX7NrNv3le3t5TJm9YzVbeRH9IggMDz6YJ5wXK59AAXsTedT5b+gsI8DMOLu0QoXWkF7F4xbNLvkbfRfG1ns5nAydgPvz7lXzOUBRTJ13Fd5hIs6G6MuTmiZGsh+uLkiktLVuPWzWeVoiSr8WQ+D8LxJJiPCD6/7zyCJscmthmsCVowZPXnC7GgE7Qff5eeKGhabaBZLKt5kqD+5fk2ItN4/q3LWGmKd+2WXa75OiJGKOtQRtRvdq7SN/5RoueVCLSGBOvEzEmdN53YOWz27jHvjp9AsjsN56PlNMW+4M39arwDLD63O37RQ6WKPrGkIYWmrX+9Nu2OQHy6lMhhKMFLbFdhU08DBeOyWmtWj8hjzsCiq4IMr/xwh+XqWuwHshCunCHjcI/GNJVwo62lVIWkeEXFb4k+aJUpy5V0tFbzksOAn9lOlihUjrsSpXVjX+t2kr1vecCEqwF5l8oOrqUVTb9wPKM2jousWuus+qNEXXI8OwqsX5G15Blc5c8Q7Gn3jFneKJDBoFqWZ3isJHUsatLby5Lba1exRnOqWev6ycqzjvN+UYwDti+gW41MSWOHFlYNhyQD/a1Adxi6N3ZtDDcWBn3EuuLbn17akjNU2xMMWg6R0gc8g2Y1vRf9hjCotieQCV6dpMhLbA3c/Lip/3sOmHHJ+mKka3pVuDw9UvW2T9C1aTDG3ezt1w8uzoCL6jOYdFBabrhMjhYGvwRaFfkWhPgpfq+5xyxCfbRgC3eqaHPl+vepB1//AwAA//8BAAD//ydb8WrgCQAA\"}]}");
        if (m is not NewLuckyBag obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void ParsePokeTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"IFTO7OvUa9:0\",\"connectionSerial\":108,\"channel\":\"15249444\",\"channelSerial\":\"11d-haoAgB4xu692134235:34544\",\"timestamp\":1637854582814,\"messages\":[{\"data\":\"H4sIAAAAAAAA/7xWzXLbNhB+F5wFmz+gSOomO3LHnbjN2EmvniWwlBGRAANAUtiMD8kh05l2poeeesop79BLX6Z+gOYROqBMiZKdNL30oBlqF/uDb7/dxRvi2gbJhKUjskJjpVZkopZVNSIK199qqS7x1RKt66Vr6W6EgcdUwDk27hGFwZfIH1O89CKt6/5/hbDCoWAh+WL4fdJeLVpssZehgqLCkwqKXiKkPRSZTdDZ60aarWUDxkkuG1AHCusMQo3mVCuF3J2BrHoV13WNyl3Y+fbwsrDcyALNTDk0A41B4MOTQtqmgvaF3Ts1X4IREtS5KvVWJsuh4bzSBVRPl3zRnsB8oKjkCq+6ZE+MBsHBPjTzfl80AhwOVVDjgR85iN/oBW7yeUMsKoHGfy0tmvMnZEKwAIhEALQMCqQMREkhyzktMo4xZGkciYRs7/sd1Egm5O7d+7u37+/e/nz39s+7d7/99fvHv//49dOHX3769OHjOzIijeRu6WtA0pN8NsviMxrEeUhZGgU0Y+OApmdns2nATpLTbHz0spmTEVEb38TzZoUVmaTJiOgGVZcn8TWYd3z23/NKm9bj1F+t0VI5MomyKM62HqJ4RCTX6sXlUzIhN841dnJ8DNaiszRMoWmOYAE1yB9RHCl0xxgkTPBI0CAQSFmYA80j4FRggYLxtIxgfNQon24BYo7n/8U5S2LgQZBTCFlEGS8ZzZIopzlAXo55FmaR6JzfDu53oQWSSTAiWvnK7ph16wHhKFe+oJtSS/tML/AEfJOVUFncVH/22vUoeTJ2cPqPa6/0P1+xDXwsz/sufC5rfCpr6ba+3E4SbHgdfrVPWc/3ceJCHW1A4vqYByWLw3FOE8CEsmAc0VwUSJMxY+OiKLiIgi0ysnTR5+NeV6toFzv/99hZWeRlCoImwDllYSpolpSclnGSsixhLAnFMHb8xdjxLnb0FcFDlsQ8LmLKsMgoE1lJi2ic0LIQecZSHrIo64OXBtHX18ff9MobssCWTLqW75K4vvGRu2kIte1pUWrlps4ZWSwdbsnz5cTKPEiycpzQOEwLyiIWURDJmGKQxxim4yRkcJjYVIkzXVV6/TDDfgpfK1wX8j7ZcnP4/033dnS/IoMRASVrcFJvJkyX0zbRew73LfaN0cuGTKLR/Qz1W62zCpOI5Ywx4veJrp7otZop4ZuHTMJxnGYJS8fRrWdFVQ33Qo1CwgVaC3Pcn/JTpfRScTxYTrsF8czoWvu8B1pn5ErC3sYSM7XCSjc4W6Fyw9iHupkSj1tediN3aLrSDvdcab74QeIajR24qNupqdvvV2hWEtcDBfq9Ou1xHy5UBHcze4110ylQwB5afvZctYoPLMDU7blaSXfoqjRob/xmPlziTX11X97PgHGJmzfBEI1aK+v2LZzeX841mFdLHC7hnkZTa6V1sFdH9IifPnh7rKRAfXoDeyWHQptLXIMRl6AWnoH3aEF7egNqx5z7kx1ZNwbD/KT1L8FLrKUSWzAWU34AXLO44nr3dNq1x/A50XPvfPpsKBcgq/Y52MUetNJKNb9ysPdI8q10ClX1IP5Wc4F1gebKgVsOWcW7K/vW2wpv/wEAAP//AQAA///cJp6Y8goAAA==\"}]}");
        if (m is not Poke obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void ParseFreshUserEnterTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"3y3KunGtEo:0\",\"connectionSerial\":3,\"channel\":\"1266543\",\"channelSerial\":\"11dJ2fCSwB5EAX07890044:15063\",\"timestamp\":1637889403240,\"messages\":[{\"data\":\"H4sIAAAAAAAA/5xVy24bNxd+F/5bTWJZo9vsZP0KoCCuDanupiiCM+SRxAxvITmjTA0vGi+Lbroq0AIBil6ALrLppu8jtN3mEYoZeySO7CxabTT8Pp4rDz9eE18aJMkg7pACreNakUTlQnSIwu1zzdUCX+fofINuud8wC49RQCka/whh8RXSx4hXFaS1bNYCocAQyDjNwu+zcpmVWGKDoYJU4JmAtEEYd8eQvQs6e2O43VsasJ5TbkAdEc5bBIl2qpVC6p8BFw1FtZSo/Llb7zfnqaOWp2hnyqMNGItAw52MOyOgvHKtXescLOOg5mql9xhfhYZroVMQL3KalWewDgjBC1zWyZ5ZDYyCe2hW+b0yDDyGFEg88sOD+EZnOG+thQjXEhmHc3QO1tiONlFK54riUZMOiV5aLbXnWgWst7zg0Oocm6kChTY4K1D5MPYxN1PsccsFrrlutbXQvlWW1TT7jOMWrQtcyHJiZXlRoC04bgMCq/OdKC7hKH+G4DezNyhNTSCDVrc8l7gsFQ0swMpyrgruj12tLLpNNSGHYbomuUPb/M//TxKS0lEPY4oRxt1RFA9OTqNxD8dRl6YjOhyuEFmP7AfuE5BIErK7/W13+/Pu7S/dYW8Ykw4xnPq8mnpCOkTdbSLVDSxQkKTbIdqgquM9MLV1b0lCnl+S+uhtWQ3gXdXXxGiuPElOQmecanW1eEESsvHeuOTpU3AOvYu6QzDmCWQggX+J7IlC/3Q87PZOMU4jRMqimMWn0WjETiM2Hp3GJ+MhjVcnT4xakw5Jga1x/q+cD/p02O9jhL14FMWjPkQphTganKR9xkb9ER13a+c3QWnnmmFdkVbVMB/O96ZDVuupFtqShPzvWf2r0jpgaW81GIwHlHSI1xmqqkMZliTZC81LhduU48vq3pFamEA6knx+TQoQ+aOHpxDZORiSrEA47BAJ5qIevzrJlVZ+4r3lae6xyfPg7cO7b3/48O6br//+9bu/vv/jz/c/7b56v3v74+729wqt2P8U4YuPBCbcLe8rvVCivPd40yFGNvhHbvgC7wQ3vOJSK+fbFl63lU+CfZ1jqHBNpyfOceehJU5Yycj0gbAXnKGebqClY5Bqu8AtWLYAleVmLwFQTjegDnJ4v5MiL/DOIMyPu+qZXaDkiu1FNpvQIzUw2ZLqw7sEjfa0tLkR1PnkMsQZcFF+Ci5rtZY7rtZLD60XyOrcTEGIB/H3zDnKFO3Sg89DqaR1ydVrvQdv/gEAAP//AQAA///rd3beTwgAAA==\"}]}");
        if (m is not FreshUserEnter obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void ParseMissionRemindTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"tGdWSOlhII:0\",\"connectionSerial\":4,\"channel\":\"1266543\",\"channelSerial\":\"11dJ2fCSwB5EAX07890044:15064\",\"timestamp\":1637889403304,\"messages\":[{\"data\":\"H4sIAAAAAAAA/3RUXW/jNhD8L3x2DsklyOX8Zrsu4KJBD1bTl6I4rMixvCd+HUnJEYL890L+pBTfmzizu1zNDvdNpM5DTJ9uJ6JFiOysmL4JdlFMhZj0Hy/eI8xdY9UBIquC4/7w+dPDp/vHCzSO3KE8fwy594mw2P3h2K7xs0FMYmobrSdix2mrAl2jSEr4dIUI+AF5jfjRQ86Z01mDWuRAzbLOv+ddUXfocMJgqdSYaypPiOI4hsLh0uWr53DO9BQSS/ZkR0RMAWQQFs5ayPQ7sT5R0hkDm55jdQ5uyigDlwhLmxAyJoBkHqk4ek3dSxxEVQ0FxWRXduPOGG/yxEq7kvSfjay7OVUZoblFsW92HhwpSfFjWl/3xStKyCkyGNXh7H7vaqwGZ63zs4FiekaMVGF428xa11iJkUiXRr8FZ1xiZzM2BW6ZBsqppW2hnceyhU353WNuadX1zDUqdgNZW5cGvxWcrP9h7BBiVsJ0s2C6v1qElrHLCPTznVk2NOpfgdJ2+Qrj9wQUDdRKbFB0VmYZFEy3si2ncalNQNz2DhmbyZvi6MpfiLHGwZu5GsbZmIYZyQ1NYij8bJCb4eT+WYwcEw3miF7xxYc30LKCW2xpMHIqXVhjR0GtydaNP6tF3WJL9uKcY6QEtzgk5P1x7BfeGob7tfQmEoUKqZdo9ZuYilI+3eNB4gYPd083D4+3n2++3uPrzZ0sn+SXLxtA3YteqD6/gIxi+niby/AmanR9IaoqqvBdOuM1Er7vQ8R+TZCJYvrvfxOxcTbNUgpcNunY//ux2KH23e37RPh6Jkdz9XUh3WXD0MlFg1d2ehqr2bccV8S6+5tiPZg8R7ZVkWiwS4Jr/IK0/nD/mXmGKRGKRKnJTS/3E+n37hl8/x8AAP//AQAA//9tMB51eAYAAA==\"}]}");
        if (m is not MissionRemind obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void ParseLiveStreamEndTest()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"7xBqCX-QXK:0\",\"connectionSerial\":236,\"channel\":\"4301955\",\"channelSerial\":\"11dVQv8VwB5EBm18218856:78318\",\"timestamp\":1637939460061,\"messages\":[{\"data\":\"H4sIAAAAAAAA/3RUy27jOBD8F59z3UtutuEFvNhgA3sz9xZZkTsim0yTkiMM5t8HSiKFVDw3sapfqi7y5yaPEZv7v+42AzRxkM299M7dbQTXfwLLCa89Up7RK+eLVbpFkTGI+QaheIG5RbxMUAh+PjvQgBLo2HTl9248dyNGzBiEGoedo2ZGLKc1pB9ND2+RdcmMpJkNR5IVkbKCPHQfRGDy38RupkzwHpIfUrsE900yyg30IBlaMAoyZaTlFB2NT6mKantSyyRHeQ4Lxs9lYutCQ+7f3nTjjtqCcDzg/D7sTgNZQ+l72lT3KVrKKCnyWNXhon8MHY7V2bny7GGZHpAStai7bUVCLwYrkb4GfdTgQ+YgBZuVB6ZKOXuQAS5EHAZILnuvuYPY25kntBwqWYeQq9/SYLofjCs0FSX8uFU//jdAB8a1IDDtdyvsaTW/BeXL4Q0+vhOwVKmV2eM8iikySP14lIHzutSzIl0mh6zNFP3505V/EOOED2+WavggKdcZOdQm8aSvPUozzO7fpsQpU7VHTIrvv92BgS3C/kLVyqkJesKV1J5Iuj4uatG4v5B8Oecz0oAHfCSU83GaXqQTPItdxOi2ZiVc7M4mfF1hmtdU2Xj23nH7WOKW2I3/U+oqaTmxtOdM1WXV0Mc9Ofet/8I8wDfQc6bcl64y7788PWwL+Os3AAAA//8BAAD//zoELv55BQAA\"}]}");
        if (m is not LiveStreamEnd obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }
    [Test]
    public void aaaa()
    {
        var m = MessageParser.Parse("{\"action\":15,\"id\":\"pBbq-ADIuc:0\",\"connectionSerial\":5,\"channel\":\"5184800\",\"channelSerial\":\"11dJ2fCSwB5EAX93992919:1054\",\"timestamp\":1638018097945,\"messages\":[{\"data\":\"H4sIAAAAAAAA/4xWS2/bOBD+Lzxbqfx+3GzHW3jR7AZ208uiKEbkSGJEkSpJyRGC/PcFJUuh3HSxN803HM7MNw/qldi6QLIZT0ekQm24kmQjSyFGROLlT8XlCX+WaGyHXrhNmYaPVEApFvYDhcZnpB8pnh2kVN7JAqFCH8g4zfzvXX3Oaqyxw1BCJHAnIOoQxs0tpFunh5eC696yAG055QXIG4WxGiFHvVdSIrV/ABediqo8R2kfTNIfLiNDNY9QH6RF7Wk0AvVPMm4KAfWTGZxKStCMgzzKWPUYj1vDV9/IiaVBfbwnGzKPJ+sJzmgQwjwOZqv5Mlgv2SoY0wUN19GSzjAmvc+/IEeyIQal5saSESk4taXLmMyXOF5jHAcwX82CWTSfBSsaRkEEGEUYxpROJ3fPRUJcbSoUZDNZjAg3346PZBODMOikz9cseoimSDMuv7QmqxEptGIldVmEjZBoyJvvCFiCT6cvZENcYPHLbgBYVZx4ktojVbIHo2SvhNKtEPsC5ZoKHF4BOq9PILPGXQrmsfPeh//YB9dD52sTeNA917a+ldvKXLH8mm/YE7BjSRtHam1hNp8+USbvxksoijuqPi1hNYY5jINxQ/54OQ8AwnUwCRfL1SyO19NxfFdIR/71vn1u/+O++WQ6BcoWAc5XEMwQl8E6pKtgtV7PKEwXE7qYXO9LhNK1VsAeFMMm4h55b8W3thebjpuE4/WP5+KHSUHjj2k3JMYRXIEF3XUvN2eUbCtET0uhuLStDx7brypDSTbjyWIdztfj2XJE8KX1QTp/hmz++b3H79cRQQvu4CuxoBO0n/9HqKKkWb2D9/EVynrDHSk3ZVSVLt7ZiFQgOLsvNViu5IMhm2kYhiMCkucNdEKjSk2xi75XdI3w5rLP3neOEv0uiUBrSNA5NKm67DqxIe3tOm5PRXc8BcnuNVzOltMMe7abhBzeARZf2tG+6qFWZZ9i0iyCprTf35qSRyC+XDnxtpLgFbYTsHMdQcH4m6w1c23yVDCw6Ksgx5t7uLfZHBfHgSyEL+fIODygMQ0TvretlKqUFG/W73ugj1rlqi1Tr7WaVxwGO5kdZIVCFXioUFrf963uINnHlidMuBos7ErZQVpa0ewbxwtq412R11ud139XqCuOF0+B7uXYdq3jPxkINj28YF40CmQwYMvyHM+1pJ6F23VHWXF7e1Ws0aRuWd0+U0XerbrfkHHC9tXz2ciVNHZoYdWwSXLQP0v0m6Gbw60x3FgY1BEd4/tfXteKM1T7FAYlh0jpE15AM7fVy35CGNT7FGSCNycp8gpbAz8+bkwzwTmXrCcj29Ib4orsTNX7PPUTPmjjrveO20cfZ8BF/RVMNqCWGy6Ts4XBb4BWZbEHIX7x32seMI9Qny3Y0u8q2qTsfpl68O1fAAAA//8BAAD//0Dpp0bUCQAA\"}]}");
        if (m is not MissionRemind obj)
        {
            Assert.Fail();
            return;
        }
        return;
    }

}
