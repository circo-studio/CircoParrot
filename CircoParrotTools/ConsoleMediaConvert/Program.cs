using System;
using System.Net;
using Amazon.MediaConvert;
using Amazon.MediaConvert.Model;

namespace ConsoleMediaConverter
{
    /* ----------------------------------------------
       * Permissions IAM user needs to run this example
       * ----------------------------------------------
       *
       {
       "Version": "2012-10-17",
       "Statement": [
       {
       "Sid": "VisualEditor0",
       "Effect": "Allow",
       "Action": [
       "mediaconvert:DescribeEndpoints",
       "mediaconvert:CreateJob"
       ],
       "Resource": "*"
       }
       ]
       }
       */
    /* --------------------------------------
    * JSON job settings used in this example
    * --------------------------------------
    * 
    {
    "Queue": "arn:aws:mediaconvert:us-west-2:505474453218:queues/Default",
    "UserMetadata": {
    "Customer": "Amazon"
    },
    "Role": "Your AWS Elemental MediaConvert role ARN",
    "Settings": {
    "OutputGroups": [
    {
    "Name": "File Group",
    "OutputGroupSettings": {
    "Type": "FILE_GROUP_SETTINGS",
    "FileGroupSettings": {
    "Destination": "s3://youroutputdestination"
    }
    },
    "Outputs": [
    {
    "VideoDescription": {
    "ScalingBehavior": "DEFAULT",
    "TimecodeInsertion": "DISABLED",
    "AntiAlias": "ENABLED",
    "Sharpness": 50,
    "CodecSettings": {
    "Codec": "H_264",
    "H264Settings": {
    "InterlaceMode": "PROGRESSIVE",
    "NumberReferenceFrames": 3,
    "Syntax": "DEFAULT",
    "Softness": 0,
    "GopClosedCadence": 1,
    "GopSize": 90,
    "Slices": 1,
    "GopBReference": "DISABLED",
    "SlowPal": "DISABLED",
    "SpatialAdaptiveQuantization": "ENABLED",
    "TemporalAdaptiveQuantization": "ENABLED",
    "FlickerAdaptiveQuantization": "DISABLED",
    "EntropyEncoding": "CABAC",
    "Bitrate": 5000000,
    "FramerateControl": "SPECIFIED",
    "RateControlMode": "CBR",
    "CodecProfile": "MAIN",
    "Telecine": "NONE",
    "MinIInterval": 0,
    "AdaptiveQuantization": "HIGH",
    "CodecLevel": "AUTO",
    "FieldEncoding": "PAFF",
    "SceneChangeDetect": "ENABLED",
    "QualityTuningLevel": "SINGLE_PASS",
    "FramerateConversionAlgorithm": "DUPLICATE_DROP",
    "UnregisteredSeiTimecode": "DISABLED",
    "GopSizeUnits": "FRAMES",
    "ParControl": "SPECIFIED",
    "NumberBFramesBetweenReferenceFrames": 2,
    "RepeatPps": "DISABLED",
    "FramerateNumerator": 30,
    "FramerateDenominator": 1,
    "ParNumerator": 1,
    "ParDenominator": 1
    }
    },
    "AfdSignaling": "NONE",
    "DropFrameTimecode": "ENABLED",
    "RespondToAfd": "NONE",
    "ColorMetadata": "INSERT"
    },
    "AudioDescriptions": [
    {
    "AudioTypeControl": "FOLLOW_INPUT",
    "CodecSettings": {
    "Codec": "AAC",
    "AacSettings": {
    "AudioDescriptionBroadcasterMix": "NORMAL",
    "RateControlMode": "CBR",
    "CodecProfile": "LC",
    "CodingMode": "CODING_MODE_2_0",
    "RawFormat": "NONE",
    "SampleRate": 48000,
    "Specification": "MPEG4",
    "Bitrate": 64000
    }
    },
    "LanguageCodeControl": "FOLLOW_INPUT",
    "AudioSourceName": "Audio Selector 1"
    }
    ],
    "ContainerSettings": {
    "Container": "MP4",
    "Mp4Settings": {
    "CslgAtom": "INCLUDE",
    "FreeSpaceBox": "EXCLUDE",
    "MoovPlacement": "PROGRESSIVE_DOWNLOAD"
    }
    },
    "NameModifier": "_1"
    }
    ]
    }
    ],
    "AdAvailOffset": 0,
    "Inputs": [
    {
    "AudioSelectors": {
    "Audio Selector 1": {
    "Offset": 0,
    "DefaultSelection": "NOT_DEFAULT",
    "ProgramSelection": 1,
    "SelectorType": "TRACK",
    "Tracks": [
    1
    ]
    }
    },
    "VideoSelector": {
    "ColorSpace": "FOLLOW"
    },
    "FilterEnable": "AUTO",
    "PsiControl": "USE_PSI",
    "FilterStrength": 0,
    "DeblockFilter": "DISABLED",
    "DenoiseFilter": "DISABLED",
    "TimecodeSource": "EMBEDDED",
    "FileInput": "s3://yourinputfile"
    }
    ],
    "TimecodeConfig": {
    "Source": "EMBEDDED"
    }
    }
    }
    12
    */
    class Program
    {
        static void Main(string[] args)
        {
            String mediaConvertRole = "arn:aws:iam::378090785532:role/TurnerMediaConvertRole";
            String fileInput = "s3://mediaconvertin/B83UV-A-03-EL-COMANDANTE---EL-COMANDANTE-ae98f140.mov";
            String fileOutput = "s3://mediaconvertout/Salida.mov";
            AmazonMediaConvertClient client;
            // Once you know what your customer endpoint is, set it here
            String mediaConvertEndpoint = "";
            // If we do not have our customer-specific endpoint
            if (String.IsNullOrEmpty(mediaConvertEndpoint))
            {
                //Amazon.Runtime.AWSCredentials c = new Amazon.Runtime.AWSCredentials();
                // Obtain the customer-specific MediaConvert endpoint
                client = new
                AmazonMediaConvertClient("AKIAJ35VKR7CZVBCL2GQ", "L2cnZrGPhvCD17X8hAbk4w2rd1sjPVtX8BE7OOmv", Amazon.RegionEndpoint.USEast1);
                DescribeEndpointsRequest describeRequest = new
                DescribeEndpointsRequest();
                DescribeEndpointsResponse describeResponse =
                client.DescribeEndpointsAsync(describeRequest).Result;
                mediaConvertEndpoint = describeResponse.Endpoints[0].Url;
            }
            // Since we have a service url for MediaConvert, we do not
            // need to set RegionEndpoint. If we do, the ServiceURL will
            // be overwritten

            client = new
                AmazonMediaConvertClient("AKIAJ35VKR7CZVBCL2GQ", "L2cnZrGPhvCD17X8hAbk4w2rd1sjPVtX8BE7OOmv", Amazon.RegionEndpoint.USEast1);

            NetworkCredential cred = new NetworkCredential("AKIAJ35VKR7CZVBCL2GQ", "L2cnZrGPhvCD17X8hAbk4w2rd1sjPVtX8BE7OOmv");


            AmazonMediaConvertConfig mcConfig = new AmazonMediaConvertConfig
            {
                ServiceURL = mediaConvertEndpoint,
                ProxyCredentials = cred
                //                ProxyCredentials = client
            };

            AmazonMediaConvertClient mcClient = new AmazonMediaConvertClient(mcConfig);
            CreateJobRequest createJobRequest = new CreateJobRequest();
            createJobRequest.Role = mediaConvertRole;
            createJobRequest.UserMetadata.Add("Customer", "Amazon");

            #region Create job settings
            JobSettings jobSettings = new JobSettings();
            jobSettings.AdAvailOffset = 0;
            jobSettings.TimecodeConfig = new TimecodeConfig();
            jobSettings.TimecodeConfig.Source = TimecodeSource.EMBEDDED;
            createJobRequest.Settings = jobSettings;

            #region OutputGroup
            OutputGroup ofg = new OutputGroup();
            ofg.Name = "File Group";
            ofg.OutputGroupSettings = new OutputGroupSettings();
            ofg.OutputGroupSettings.Type = OutputGroupType.FILE_GROUP_SETTINGS;
            ofg.OutputGroupSettings.FileGroupSettings = new FileGroupSettings();
            ofg.OutputGroupSettings.FileGroupSettings.Destination = fileOutput;
            Output output = new Output();
            output.NameModifier = "_1";
            #region VideoDescription
            VideoDescription vdes = new VideoDescription();
            output.VideoDescription = vdes;
            vdes.ScalingBehavior = ScalingBehavior.DEFAULT;
            vdes.TimecodeInsertion = VideoTimecodeInsertion.DISABLED;
            vdes.AntiAlias = AntiAlias.ENABLED;
            vdes.Sharpness = 50;
            vdes.AfdSignaling = AfdSignaling.NONE;

            vdes.DropFrameTimecode = DropFrameTimecode.ENABLED;
            vdes.RespondToAfd = RespondToAfd.NONE;
            vdes.ColorMetadata = ColorMetadata.INSERT;
            vdes.CodecSettings = new VideoCodecSettings();
            vdes.CodecSettings.Codec = VideoCodec.H_264;
            H264Settings h264 = new H264Settings();
            h264.InterlaceMode = H264InterlaceMode.PROGRESSIVE;
            h264.NumberReferenceFrames = 3;
            h264.Syntax = H264Syntax.DEFAULT;
            h264.Softness = 0;
            h264.GopClosedCadence = 1;
            h264.GopSize = 90;
            h264.Slices = 1;
            h264.GopBReference = H264GopBReference.DISABLED;
            h264.SlowPal = H264SlowPal.DISABLED;
            h264.SpatialAdaptiveQuantization =
            H264SpatialAdaptiveQuantization.ENABLED;
            h264.TemporalAdaptiveQuantization =
            H264TemporalAdaptiveQuantization.ENABLED;
            h264.FlickerAdaptiveQuantization =
            H264FlickerAdaptiveQuantization.DISABLED;
            h264.EntropyEncoding = H264EntropyEncoding.CABAC;
            h264.Bitrate = 5000000;
            h264.FramerateControl = H264FramerateControl.SPECIFIED;
            h264.RateControlMode = H264RateControlMode.CBR;
            h264.CodecProfile = H264CodecProfile.MAIN;
            h264.Telecine = H264Telecine.NONE;
            h264.MinIInterval = 0;
            h264.AdaptiveQuantization = H264AdaptiveQuantization.HIGH;
            h264.CodecLevel = H264CodecLevel.AUTO;
            h264.FieldEncoding = H264FieldEncoding.PAFF;
            h264.SceneChangeDetect = H264SceneChangeDetect.ENABLED;
            h264.QualityTuningLevel = H264QualityTuningLevel.SINGLE_PASS;
            h264.FramerateConversionAlgorithm =
            H264FramerateConversionAlgorithm.DUPLICATE_DROP;
            h264.UnregisteredSeiTimecode = H264UnregisteredSeiTimecode.DISABLED;
            h264.GopSizeUnits = H264GopSizeUnits.FRAMES;
            h264.ParControl = H264ParControl.SPECIFIED;
            h264.NumberBFramesBetweenReferenceFrames = 2;
            h264.RepeatPps = H264RepeatPps.DISABLED;
            h264.FramerateNumerator = 30;
            h264.FramerateDenominator = 1;
            h264.ParNumerator = 1;
            h264.ParDenominator = 1;
            output.VideoDescription.CodecSettings.H264Settings = h264;
            #endregion VideoDescription
            #region AudioDescription
            AudioDescription ades = new AudioDescription();
            ades.LanguageCodeControl = AudioLanguageCodeControl.FOLLOW_INPUT;
            // This name matches one specified in the Inputs below
            ades.AudioSourceName = "Audio Selector 1";
            ades.CodecSettings = new AudioCodecSettings();
            ades.CodecSettings.Codec = AudioCodec.AAC;
            AacSettings aac = new AacSettings();
            aac.AudioDescriptionBroadcasterMix =
            AacAudioDescriptionBroadcasterMix.NORMAL;
            aac.RateControlMode = AacRateControlMode.CBR;
            aac.CodecProfile = AacCodecProfile.LC;
            aac.CodingMode = AacCodingMode.CODING_MODE_2_0;
            aac.RawFormat = AacRawFormat.NONE;
            aac.SampleRate = 48000;
            aac.Specification = AacSpecification.MPEG4;
            aac.Bitrate = 64000;
            ades.CodecSettings.AacSettings = aac;
            output.AudioDescriptions.Add(ades);

            #endregion AudioDescription
            #region Mp4 Container
            output.ContainerSettings = new ContainerSettings();
            output.ContainerSettings.Container = ContainerType.MP4;
            Mp4Settings mp4 = new Mp4Settings();
            mp4.CslgAtom = Mp4CslgAtom.INCLUDE;
            mp4.FreeSpaceBox = Mp4FreeSpaceBox.EXCLUDE;
            mp4.MoovPlacement = Mp4MoovPlacement.PROGRESSIVE_DOWNLOAD;
            output.ContainerSettings.Mp4Settings = mp4;
            #endregion Mp4 Container
            ofg.Outputs.Add(output);
            createJobRequest.Settings.OutputGroups.Add(ofg);
            #endregion OutputGroup
            #region Input
            Input input = new Input();
            input.FilterEnable = InputFilterEnable.AUTO;
            input.PsiControl = InputPsiControl.USE_PSI;
            input.FilterStrength = 0;
            input.DeblockFilter = InputDeblockFilter.DISABLED;
            input.DenoiseFilter = InputDenoiseFilter.DISABLED;
            input.TimecodeSource = InputTimecodeSource.EMBEDDED;
            input.FileInput = fileInput;
            AudioSelector audsel = new AudioSelector();
            audsel.Offset = 0;
            audsel.DefaultSelection = AudioDefaultSelection.NOT_DEFAULT;
            audsel.ProgramSelection = 1;
            audsel.SelectorType = AudioSelectorType.TRACK;
            audsel.Tracks.Add(1);
            input.AudioSelectors.Add("Audio Selector 1", audsel);
            input.VideoSelector = new VideoSelector();
            input.VideoSelector.ColorSpace = ColorSpace.FOLLOW;
            createJobRequest.Settings.Inputs.Add(input);

            #endregion Input
            #endregion Create job settings
            try
            {
                CreateJobResponse createJobResponse =
                mcClient.CreateJobAsync(createJobRequest).Result;
                Console.WriteLine("Job Id: {0}", createJobResponse.Job.Id);
            }
            catch (BadRequestException bre)
            {
                // If the enpoint was bad
                if (bre.Message.StartsWith("You must use the customer-"))
                {
                    // The exception contains the correct endpoint; extract it
                    mediaConvertEndpoint = bre.Message.Split('\'')[1];
                    // Code to retry query
                }
            }
        }
    }
}
